using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RAP.Core.Common
{
  public class ExceptionHandler : RAP.Core.Common.IExceptionHandler
  {
      public OperationStatus HandleException(Exception ex )
      {
          StringBuilder exceptionMessageDetails = new StringBuilder();
          if (ex.InnerException != null)
          {
              while (ex.InnerException != null) ex = ex.InnerException;
              //exceptionMessageDetails.Append(ex.InnerException.Message).Append("-").Append(ex.InnerException.StackTrace);
          }

          exceptionMessageDetails.Append(ex.Message).Append("-").Append(ex.StackTrace);

          if (ex.GetType() == typeof(TimeoutException))
          {
             return new OperationStatus() { Status = StatusEnum.TimeoutException, StatusDetails = exceptionMessageDetails.ToString() };
          }
          else if (ex.GetType() == typeof(OutOfMemoryException))
          {
             return new OperationStatus() { Status = StatusEnum.SystemException, StatusDetails = exceptionMessageDetails.ToString() };
          }
          else if (ex.GetType() == typeof(ArgumentNullException))
          {
              return new OperationStatus() { Status = StatusEnum.NullArgumentException, StatusDetails = exceptionMessageDetails.ToString() };
          }
          else if (ex.GetType() == typeof(ArgumentException))
          {
              return new OperationStatus() { Status = StatusEnum.InvalidArgumentException, StatusDetails = exceptionMessageDetails.ToString() };
          }        
          else if (ex.GetType() == typeof(SqlException))
          {
            return new OperationStatus() { Status = StatusEnum.TimeoutException, StatusDetails = exceptionMessageDetails.ToString() };
          }
          else if (ex.GetType() == typeof(WebException))
          {
             return new OperationStatus() { Status = StatusEnum.CommunicationException, StatusDetails = exceptionMessageDetails.ToString() };
          }
          else if (ex.GetType() == typeof(FormatException))
          {
            return new OperationStatus() { Status = StatusEnum.InvalidArgumentException, StatusDetails = exceptionMessageDetails.ToString() };
          }
          else
          {
              return new OperationStatus() { Status = StatusEnum.SystemException, StatusDetails = exceptionMessageDetails.ToString() };
          }
      }
  }
}
