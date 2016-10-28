using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Globalization;
using System.Data.SqlClient;

namespace RAP.Persistence.ADO
{
    public class DbParameters<TParameter> : ArrayList, IDbParameters
        where TParameter : DbParameter, new()
    {
        public DbParameters() { }

        public IDataParameter this[string parameterName]
        {
            get
            {
                foreach (TParameter item in this)
                {
                    if (0 == _cultureAwareCompare(item.ParameterName, parameterName))
                    {
                        return item;
                    }
                }
                return null;
            }
        }

        //public bool Contains(string parameterName)
        //{
        //    return (-1 != IndexOf(parameterName));
        //}

        //public int IndexOf(string parameterName)
        //{
        //    int index = 0;
        //    foreach (TParameter item in this)
        //    {
        //        if (0 == _cultureAwareCompare(item.ParameterName, parameterName))
        //        {
        //            return index;
        //        }
        //        index++;
        //    } return -1;
        //}

        //public void RemoveAt(string parameterName)
        //{
        //    RemoveAt(IndexOf(parameterName));
        //}

        public override int Add(object value)
        {
            return Add((TParameter)value);
        }

        public int Add(DbParameter value)
        {
            if (((TParameter)value).ParameterName != null)
            {
                return base.Add(value);
            }
            else
                throw new ArgumentException("UnnamedParameter");
        }

        public int AddInParameter(string parameterName, object value)
        {
            return Add(new TParameter()
            {
                ParameterName = parameterName,
                Value = value
            });
        }

        public int AddInParameter(string parameterName, Enum parameterType, object value)
        {
            if (typeof(TParameter) == typeof(SqlParameter))
            {
                return Add(new SqlParameter()
                {
                    ParameterName = parameterName,
                    SqlDbType = (SqlDbType)parameterType,
                    Value = value,
                    Direction = ParameterDirection.Input
                });
            }
            else
            {
                return Add(new TParameter()
                {
                    ParameterName = parameterName,
                    DbType = (DbType)parameterType,
                    Value = value,
                    Direction = ParameterDirection.Input
                });
            }
        }

        public int AddInOutParameter(string parameterName, Enum parameterType, object value)
        {
            if (typeof(TParameter) == typeof(SqlParameter))
            {
                return Add(new SqlParameter()
                {
                    ParameterName = parameterName,
                    SqlDbType = (SqlDbType)parameterType,
                    Value = value,
                    Direction = ParameterDirection.InputOutput
                });
            }
            else
            {
                return Add(new TParameter()
                {
                    ParameterName = parameterName,
                    DbType = (DbType)parameterType,
                    Value = value,
                    Direction = ParameterDirection.InputOutput
                });
            }
        }

        public int AddInParameter(string parameterName, Enum parameterType, object value, int size)
        {
            if (typeof(TParameter) == typeof(SqlParameter))
            {
                return Add(new SqlParameter()
                {
                    ParameterName = parameterName,
                    SqlDbType = (SqlDbType)parameterType,
                    Value = value,
                    Size = size,
                    Direction = ParameterDirection.Input
                });
            }
            else
            {
                return Add(new TParameter()
                {
                    ParameterName = parameterName,
                    DbType = (DbType)parameterType,
                    Value = value,
                    Size = size,
                    Direction = ParameterDirection.Input
                });
            }
        }

        public int AddOutParameter(string parameterName, Enum parameterType, int size = Int32.MaxValue)
        {
            if (typeof(TParameter) == typeof(SqlParameter))
            {
                return Add(new SqlParameter()
                {
                    ParameterName = parameterName,
                    SqlDbType = (SqlDbType)parameterType,
                    Size = size,
                    Direction = ParameterDirection.Output
                });
            }
            else
            {
                return Add(new TParameter()
                {
                    ParameterName = parameterName,
                    DbType = (DbType)parameterType,
                    Size = size,
                    Direction = ParameterDirection.Output
                });
            }
        }

        private int _cultureAwareCompare(string strA, string strB)
        {
            return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.IgnoreCase);
        }
    }
}
