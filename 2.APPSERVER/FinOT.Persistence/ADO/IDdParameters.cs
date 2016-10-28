using System;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace RAP.Persistence.ADO
{
    
    public interface IDbParameters : IList, ICollection, IEnumerable
    {
        IDataParameter this[string parameterName] { get; }

        int Add(DbParameter value);
        int AddInParameter(string parameterName, object value);
        int AddInParameter(string parameterName, Enum parameterType, object value);
        int AddInOutParameter(string parameterName, Enum parameterType, object value);
        int AddInParameter(string parameterName, Enum parameterType, object value, int size);
        int AddOutParameter(string parameterName, Enum parameterType, int size = Int32.MaxValue);
    }

}
