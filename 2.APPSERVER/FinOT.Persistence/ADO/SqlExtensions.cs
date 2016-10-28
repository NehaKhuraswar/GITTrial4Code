using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace RAP.Persistence.ADO
{
    internal static class SqlExtensions
    {
        public static void CheckReturnMessage(this SqlCommand cmd)
        {
            if (cmd.Parameters != null)
            {
                if (cmd.Parameters["Message"] != null)
                {
                    if ((cmd.Parameters["Message"].Value != null) && (cmd.Parameters["Message"].Value != DBNull.Value))
                    {
                        string exceptionDetails = cmd.Parameters["Message"].Value.ToString();
                        if (!string.IsNullOrEmpty(exceptionDetails))
                        {
                            throw new ApplicationException(exceptionDetails);
                        }
                    }
                }
            }
        }
        
    }
}
