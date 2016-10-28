using System;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace RAP.Persistence.ADO
{
    internal abstract class Database : IDatabase
    {

        static ConnectionStringSettingsCollection connectionsList;
        protected DbProviderFactory provider;
        protected IDbConnection connection;

        public Database()
        {
            if (connectionsList == null)
            {
                connectionsList = ConfigurationManager.ConnectionStrings;
            }
        }

        public IDbConnection CreateConnection(string connectionName)
        {
            ConnectionStringSettings settings = null;
            foreach (ConnectionStringSettings item in connectionsList)
            {
                if (item.Name == connectionName)
                {
                    settings = item;
                }
            }

            if (settings != null)
            {
                provider = DbProviderFactories.GetFactory(settings.ProviderName);
                connection = provider.CreateConnection();
                connection.ConnectionString = settings.ConnectionString;
            }

            return connection;
        }

        public virtual IDbCommand CreateCommand()
        {
            IDbCommand command = provider.CreateCommand();
            command.Connection = connection;
            return command;
        }

        public virtual IDbDataAdapter CreateDataAdapter()
        {
            return provider.CreateDataAdapter();
        }

        public virtual IDbParameters CreateDBParameters()
        {
            throw new NotImplementedException();
        }

        public virtual IDataReader ExecuteReader(string sqlString, CommandType commandType, IDbParameters paramCollection = null)
        {
            throw new NotImplementedException();
        }

        public virtual int ExecuteNonQuery(string sqlString, CommandType commandType, IDbParameters paramCollection = null)
        {
            throw new NotImplementedException();
        }

        public virtual string ExecuteScalar(string sqlString, CommandType commandType, IDbParameters paramCollection = null)
        {
            throw new NotImplementedException();
        }

        public virtual int FillDataSet(DataSet dataSet, string sqlString, CommandType commandType, IDbParameters paramCollection = null)
        {
            throw new NotImplementedException();
        }

        public virtual int FillDataTable(DataTable dataTable, string sqlString, CommandType commandType, IDbParameters paramCollection = null)
        {
            throw new NotImplementedException();
        }

    }
}
