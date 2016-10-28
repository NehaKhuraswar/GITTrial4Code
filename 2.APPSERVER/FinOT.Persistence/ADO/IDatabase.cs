using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace RAP.Persistence.ADO
{
    public interface IDatabase
    {
        //string connectionName { get; set; }

        IDbConnection CreateConnection(string connectionName);
        IDbCommand CreateCommand();
        IDbDataAdapter CreateDataAdapter();
        IDbParameters CreateDBParameters();

        IDataReader ExecuteReader(string sqlString, CommandType commandType, IDbParameters paramCollection = null);
        int ExecuteNonQuery(string sqlString, CommandType commandType, IDbParameters paramCollection = null);
        int FillDataSet(DataSet dataSet, string sqlString, CommandType commandType, IDbParameters paramCollection = null);
        int FillDataTable(DataTable dataTable, string sqlString, CommandType commandType, IDbParameters paramCollection = null);
    }
}
