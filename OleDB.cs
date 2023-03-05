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
            int bits = IntPtr.Size * 8;

            // May need to install this: https://www.microsoft.com/en-us/download/details.aspx?id=13255

            var connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\git\CSHARP\ConvertDataPath2Wex\Files\Example1\EAB_MultiER_105.xls;Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";";

            using (var db = new Database.Database(connectionString, new Database.DatabaseProviderOleDb()))
            {
                db.Connect();

                var connection = db.Connection as System.Data.OleDb.OleDbConnection;
                var userTables = connection.GetSchema("Tables");

                string tableName = null;

                //var columns=new List<string>();
                //for (int i = 0; i < userTables.Columns.Count; i++)
                //    columns.Add(userTables.Columns[i].ColumnName);

                for (int i = 0; i < userTables.Rows.Count; i++)
                    tableName = userTables.Rows[i][2].ToString();

                using (var dr = db.RunProcDatareader("select * from [" + tableName + "]", System.Data.CommandType.Text))
                {
                    var columns=new List<string>();
                    for (int i = 0; i < dr.FieldCount; i++)
                        columns.Add(dr.GetName(i));

                    var dbDr = dr as DbDataReader;
                    var list = (dr as DbDataReader).ToList();

                    var er_key = list.First()["er_key"];
                }
                db.Disconnect();
            }
        }
    }
}
