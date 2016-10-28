using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;

namespace RAP.Persistence.ADO
{
    internal class SqlBase : Database
    {
        public SqlBase()  { }

        private SqlCommand CreateSqlCommand(string sqlString, CommandType commandType)
        {
            SqlCommand command = (SqlCommand)CreateCommand();
            command.CommandType = commandType;
            command.CommandText = sqlString;
            return command;
        }
        
        private SqlDataAdapter CreateSqlDataAdapter()
        {
            return (SqlDataAdapter)CreateDataAdapter();
        }

        public override IDbParameters CreateDBParameters()
        {
            return new DbParameters<SqlParameter>();
        }

        public override IDataReader ExecuteReader(string sqlString, CommandType commandType, IDbParameters paramCollection = null)
        {
            SqlCommand command = CreateSqlCommand(sqlString, commandType);
            if (paramCollection != null)
            {
                foreach (SqlParameter param in paramCollection)
                {
                    command.Parameters.Add(param);
                }
            }
            command.Connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public override int ExecuteNonQuery(string sqlString, CommandType commandType, IDbParameters paramCollection = null)
        {
            SqlCommand command = null;
            try
            {
                command = CreateSqlCommand(sqlString, commandType);
                if (paramCollection != null)
                {
                    foreach (SqlParameter param in paramCollection)
                    {
                        command.Parameters.Add(param);
                    }
                }
                command.Connection.Open();
                int count = command.ExecuteNonQuery();
                command.CheckReturnMessage();
                if (paramCollection != null)
                {
                    foreach (SqlParameter param in paramCollection)
                    {
                        if (param.Direction == ParameterDirection.Output && param.ParameterName != "Message")
                        {
                            param.Value = command.Parameters[param.ParameterName].Value;
                        }
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    if (command.Connection.State != ConnectionState.Closed)
                    {
                        command.Connection.Close();
                    }
                }
            }
        }

        public override string ExecuteScalar(string sqlString, CommandType commandType, IDbParameters paramCollection = null)
        {
            SqlCommand command = null;
            try
            {
                command = CreateSqlCommand(sqlString, commandType);
                if (paramCollection != null)
                {
                    foreach (SqlParameter param in paramCollection)
                    {
                        command.Parameters.Add(param);
                    }
                }
                command.Connection.Open();
                string returnVal = (string)command.ExecuteScalar();
                command.CheckReturnMessage();
                if (paramCollection != null)
                {
                    foreach (SqlParameter param in paramCollection)
                    {
                        if (param.Direction == ParameterDirection.Output && param.ParameterName != "Message")
                        {
                            param.Value = command.Parameters[param.ParameterName].Value;
                        }
                    }
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    if (command.Connection.State != ConnectionState.Closed)
                    {
                        command.Connection.Close();
                    }
                }
            }
        }

        public override int FillDataSet(DataSet dataSet, string sqlString, CommandType commandType, IDbParameters paramCollection)
        {
            SqlDataAdapter adapter = CreateSqlDataAdapter();
            SqlCommand command = CreateSqlCommand(sqlString, commandType);
            if (paramCollection != null)
            {
                foreach (SqlParameter param in paramCollection)
                {
                    command.Parameters.Add(param);
                }
            }
            adapter.SelectCommand = command;
            int count = adapter.Fill(dataSet);
            command.CheckReturnMessage();
            return count;
        }

        public override int FillDataTable(DataTable dataTable, string sqlString, CommandType commandType, IDbParameters paramCollection)
        {
            SqlDataAdapter adapter = CreateSqlDataAdapter();
            SqlCommand command = CreateSqlCommand(sqlString, commandType);
            if (paramCollection != null)
            {
                foreach (SqlParameter param in paramCollection)
                {
                    command.Parameters.Add(param);
                }
            }
            adapter.SelectCommand = command;
            int count = adapter.Fill(dataTable);
            command.CheckReturnMessage();
            return count;
        }
    }
}
