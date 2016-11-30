using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;

namespace RAP.Core.Common
{
  public class ExceptionHandler : RAP.Core.Common.IExceptionHandler
  {
      public OperationStatus HandleException(Exception ex)
      {
          bool logToEventViewer = string.IsNullOrEmpty(ConfigurationManager.AppSettings["logToEventViewer"]) ? false : ((ConfigurationManager.AppSettings["logToEventViewer"] == "true" ? true : false));
          StringBuilder exceptionMessageDetails = new StringBuilder();
          OperationStatus status;
          if (ex.InnerException != null)
          {
              while (ex.InnerException != null) ex = ex.InnerException;
              //exceptionMessageDetails.Append(ex.InnerException.Message).Append("-").Append(ex.InnerException.StackTrace);
          }

          exceptionMessageDetails.Append(ex.Message).Append("-").Append(ex.StackTrace.ToString());

          if (ex.GetType() == typeof(TimeoutException))
          {
              status = new OperationStatus() { Status = StatusEnum.TimeoutException, StatusDetails = exceptionMessageDetails.ToString() };
          }
          else if (ex.GetType() == typeof(OutOfMemoryException))
          {
              status = new OperationStatus() { Status = StatusEnum.SystemException, StatusDetails = exceptionMessageDetails.ToString() };
          }
          else if (ex.GetType() == typeof(ArgumentNullException))
          {
              status = new OperationStatus() { Status = StatusEnum.NullArgumentException, StatusDetails = exceptionMessageDetails.ToString() };
          }
          else if (ex.GetType() == typeof(ArgumentException))
          {
              status = new OperationStatus() { Status = StatusEnum.InvalidArgumentException, StatusDetails = exceptionMessageDetails.ToString() };
          }
          else if (ex.GetType() == typeof(SqlException))
          {
              status = new OperationStatus() { Status = StatusEnum.TimeoutException, StatusDetails = exceptionMessageDetails.ToString() };
          }
          else if (ex.GetType() == typeof(WebException))
          {
              status = new OperationStatus() { Status = StatusEnum.CommunicationException, StatusDetails = exceptionMessageDetails.ToString() };
          }
          else if (ex.GetType() == typeof(FormatException))
          {
              status = new OperationStatus() { Status = StatusEnum.InvalidArgumentException, StatusDetails = exceptionMessageDetails.ToString() };
          }
          else
          {
              status = new OperationStatus() { Status = StatusEnum.SystemException, StatusDetails = exceptionMessageDetails.ToString() };
          }

          if (logToEventViewer)
          {
              System.Diagnostics.EventLog.WriteEntry("Application", "ErrorNumber : " + status.StatusCode + " | ErrorMessage : " + status.StatusMessage + " | ErrorDetails : " + status.StatusDetails);
          }

          return status;
      }
  }
}
