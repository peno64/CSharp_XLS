using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace Database
{
    public interface IDatabaseProvider
    {
        IDbConnection CreateConnectionObject(string connstring);

        IDbCommand CreateCommandObject();

        IDbDataAdapter CreateDataAdapter(IDbCommand cmd);

        IDbDataParameter CreateParameter(string parameterName, object value);

        IDbDataParameter CreateParameter(string parameterName, DbType dbtype);

        IDbDataParameter CreateParameter(string parameterName, DbType dbtype, object value);
    }
}
