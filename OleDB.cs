using Database;
using Spire.Xls.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CSharp_XLS
{
    public static class OleDB
    {
        public static void ReadData()
        {
            var connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\git\CSHARP\ConvertDataPath2Wex\Files\Example1\EAB_MultiER_105.xls;Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";";

            using (var db = new Database.Database(connectionString, new Database.DatabaseProviderOleDb()))
            {
                db.Connect();

                var connection = db.Connection as System.Data.OleDb.OleDbConnection;
                var userTables = connection.GetSchema("Tables");

                string tableName = null;

                for (int i = 0; i < userTables.Rows.Count; i++)
                    tableName = userTables.Rows[i][2].ToString();

                using (var dr = db.RunProcDatareader("select * from [" + tableName + "]", System.Data.CommandType.Text))
                {
                    var dbDr = dr as DbDataReader;
                    var list = dbDr.ToList();
                    var er_key = list.First()["er_key"];
                }
                db.Disconnect();
            }
        }
    }
}
