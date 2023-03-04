using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;

namespace Database
{
    class DatabaseProviderOleDb : IDatabaseProvider
    {
        public IDbConnection CreateConnectionObject(string connstring)
        {
            return new OleDbConnection(connstring);
        }

        public IDbCommand CreateCommandObject()
        {
            return new OleDbCommand();
        }

        public IDbCommand CreateCommandObject(string cmdText, IDbConnection connection)
        {
            return new OleDbCommand(cmdText, (OleDbConnection) connection);
        }

        public IDbDataAdapter CreateDataAdapter(IDbCommand cmd)
        {
            return new OleDbDataAdapter((OleDbCommand)cmd);
        }

        public IDbDataParameter CreateParameter(string parameterName, object value)
        {
            return new OleDbParameter(parameterName, (value == null) ? DBNull.Value : value);
        }

        public IDbDataParameter CreateParameter(string parameterName, DbType dbtype)
        {
            return (dbtype == DbType.Date) ? new OleDbParameter(parameterName, OleDbType.Date) : new OleDbParameter(parameterName, dbtype);
        }

        public IDbDataParameter CreateParameter(string parameterName, DbType dbtype, object value)
        {
            var ret = (dbtype == DbType.Date) ? new OleDbParameter(parameterName, OleDbType.Date) : new OleDbParameter(parameterName, dbtype);
            ret.Value = (value == null) ? DBNull.Value : value;
            return ret;
        }
    }
}