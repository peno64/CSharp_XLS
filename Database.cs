using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Data.SqlClient;

namespace Database
{
    public class Database : IDisposable
    {
        private string connstring;
        private IDbTransaction transaction = null;
        private IDatabaseProvider databaseProvider;

        public IDbConnection Connection { get; set; }

        public Database(string ConnectionString, IDatabaseProvider DatabaseProvider)
        {
            connstring = ConnectionString;
            databaseProvider = DatabaseProvider;
            Connection = databaseProvider.CreateConnectionObject(connstring);
        }

        public IDbDataParameter CreateParameter(string parameterName, object value)
        {
            return databaseProvider.CreateParameter(parameterName, value);
        }

        public IDbDataParameter CreateParameter(string parameterName, DbType dbtype)
        {
            return databaseProvider.CreateParameter(parameterName, dbtype);
        }

        public IDbDataParameter CreateParameter(string parameterName, DbType dbtype, object value)
        {
            return databaseProvider.CreateParameter(parameterName, dbtype, value);
        }

        public string ConnectionString
        {
            get { return this.connstring; }
        }

        public void Connect()
        {
            try
            {
                Connection.Open();
            }
            catch (Exception ex)
            {
                Connection.Dispose();
                throw ex;
            }
        }

        private void ConnectIfNeeded()
        {
            if (this.Connection.State != ConnectionState.Open)
            {
                this.Connect();
            }
        }

        public void BeginTransaction()
        {
            ConnectIfNeeded();

            transaction = Connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (transaction != null)
                transaction.Commit();
        }

        public void RollbackTransaction(DbTransaction transaction)
        {
            if (transaction != null)
                transaction.Rollback();
        }

        public void Disconnect()
        {
            try
            {
                Connection.Close();
            }
            catch (Exception ex)
            {
                Connection.Dispose();
                throw ex;
            }
        }

        public DataSet RunProcDataSet(string str, CommandType CommandType, params IDbDataParameter[] parms)
        {
            ConnectIfNeeded();

            using (IDbCommand cmd = databaseProvider.CreateCommandObject())
            {
                DataSet ds = new DataSet();
                cmd.Connection = this.Connection;
                cmd.Transaction = transaction;
                cmd.Parameters.AddRange(parms);
                cmd.CommandType = CommandType;
                cmd.CommandText = (str + "").Trim();
                var da = databaseProvider.CreateDataAdapter(cmd);
                da.Fill(ds);
                var dda = da as IDisposable;
                if (dda != null)
                    dda.Dispose();
                return ds;
            }
        }

        public IDataReader RunProcDatareader(string str, CommandType CommandType, params IDbDataParameter[] parms)
        {
            ConnectIfNeeded();

            using (IDbCommand cmd = databaseProvider.CreateCommandObject())
            {
                cmd.Connection = this.Connection;
                cmd.Transaction = transaction;
                cmd.Parameters.AddRange(parms);
                cmd.CommandType = CommandType;
                cmd.CommandText = (str + "").Trim();
                cmd.CommandTimeout = 0;
                return cmd.ExecuteReader(CommandBehavior.Default);
            }
        }

        public object RunProcScalar(string str, CommandType CommandType, params IDbDataParameter[] parms)
        {
            ConnectIfNeeded();
            using (IDbCommand cmd = databaseProvider.CreateCommandObject())
            {
                cmd.Connection = this.Connection;
                cmd.Transaction = transaction;
                cmd.Parameters.AddRange(parms);
                cmd.CommandType = CommandType;
                cmd.CommandText = (str + "").Trim();
                return cmd.ExecuteScalar();
            }
        }


        public int RunProcNonQuery(string str, CommandType CommandType, params IDbDataParameter[] parms)
        {
            ConnectIfNeeded();
            using (IDbCommand cmd = databaseProvider.CreateCommandObject())
            {
                cmd.Connection = this.Connection;
                cmd.Transaction = transaction;
                cmd.Parameters.AddRange(parms);
                cmd.CommandType = CommandType;
                cmd.CommandText = (str + "").Trim();
                return cmd.ExecuteNonQuery();
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                Connection.Dispose();
            }
            catch
            {
                Connection = null;
            }
        }

        #endregion
    }

    public static class DatabaseExtensions
    {
        public static void AddRange(this IDataParameterCollection parameters, params IDbDataParameter[] parms)
        {
            foreach (var parm in parms)
                parameters.Add(parm);
        }

        public static List<IDataRecord> ToList(this DbDataReader dbDataReader)
        {
            return (from IDataRecord t in dbDataReader select t).ToList();
        }
    }
}
