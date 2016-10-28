using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using RAP.Core.FinServices.LogService;
using RAP.Core.Common;

namespace RAP.API.Common
{
    internal sealed class LogHelper
    {
        static object syncRoot = new object();

        static LogHelper()
        {
        }

        static LogHelper instance;
        internal static LogHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new LogHelper();
                    }
                }
                return instance;
            }
        }

        internal void Debug(string CorrelationId, string Username, string action, string message, string messageDetails, int elapsedTime, Exception exception)
        {
            LogDetails logDetails = new LogDetails();
            logDetails.Action = action;
            logDetails.ElapsedTime = elapsedTime;
            logDetails.ExceptionDetails = exception != null ? exception.ToString() : null;
            logDetails.Message = message;
            logDetails.MessageDetails = messageDetails;
            logDetails.Severity = "DEBUG";
            logDetails.SendNotification = false;
            logDetails.RequesterDetails = GetRequester(CorrelationId, Username);
            Log(logDetails);
        }

        internal void Error(string CorrelationId, string Username, string action, string message, string messageDetails, int elapsedTime, Exception exception)
        {
            LogDetails logDetails = new LogDetails();
            logDetails.Action = action;
            logDetails.ElapsedTime = elapsedTime;
            logDetails.ExceptionDetails = exception != null ? exception.ToString() : null;
            logDetails.Message = message;
            logDetails.MessageDetails = messageDetails;
            logDetails.Severity = "ERROR";
            logDetails.SendNotification = true;
            logDetails.RequesterDetails = GetRequester(CorrelationId, Username);
            Log(logDetails);
        }

        internal void Info(string CorrelationId, string Username, string action, string message, string messageDetails, int elapsedTime)
        {
            LogDetails logDetails = new LogDetails();
            logDetails.Action = action;
            logDetails.ElapsedTime = elapsedTime;
            logDetails.Message = message;
            logDetails.MessageDetails = messageDetails;
            logDetails.Severity = "INFO";
            logDetails.SendNotification = false;
            logDetails.RequesterDetails = GetRequester(CorrelationId, Username);
            Log(logDetails);
        }

        internal void Warn(string CorrelationId, string Username, string action, string message, string messageDetails, int elapsedTime)
        {
            LogDetails logDetails = new LogDetails();
            logDetails.Action = action;
            logDetails.ElapsedTime = elapsedTime;
            logDetails.Message = message;
            logDetails.MessageDetails = messageDetails;
            logDetails.Severity = "WARN";
            logDetails.SendNotification = false;
            logDetails.RequesterDetails = GetRequester(CorrelationId, Username);
            Log(logDetails);
        }

        void Log(LogDetails logDetails)
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    LogToService(logDetails);
                });
            }
            catch (Exception logException)
            {
                Task.Factory.StartNew(() =>
                {
                    LogToFile(logDetails, logException);
                });
            }
            finally
            {
            }
        }

        void LogToService(LogDetails logDetails)
        {
            LogServiceClient client = null;
            try
            {
                client = new LogServiceClient(WebConfigurationManager.AppSettings[Constants.FINLOGSERVICE_ENDPOINT]);
                client.Open();
                client.CreateLogEntry(logDetails);
            }
            finally
            {
                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    try
                    {
                        client.Close();
                    }
                    catch
                    {
                    }
                }
                client = null;
            }
        }

        void LogToFile(LogDetails logDetails, Exception exception)
        {
            string logFolderPath = HttpContext.Current.Server.MapPath("~/Logs");
            string logFilePath = Path.Combine(logFolderPath, logDetails.RequesterDetails.CorrelationId, ".log");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Timestamp\t{0}", DateTime.Now.ToString()).AppendLine();
            sb.AppendFormat("Correlation\t{0}", logDetails.RequesterDetails.CorrelationId).AppendLine();
            sb.AppendFormat("Machine Name\t{0}", logDetails.RequesterDetails.MachineName).AppendLine();
            sb.AppendFormat("Process Name\t{0}", logDetails.RequesterDetails.ProcessName).AppendLine();
            sb.AppendFormat("Machine Name\t{0}", logDetails.RequesterDetails.MachineName).AppendLine();
            sb.AppendFormat("Username\t{0}", logDetails.RequesterDetails.Username).AppendLine();
            sb.AppendFormat("Severity\t{0}", logDetails.Severity).AppendLine();
            sb.AppendFormat("Action\t{0}", logDetails.Action).AppendLine();
            sb.AppendFormat("Elapsed Time\t{0}", logDetails.ElapsedTime).AppendLine();
            sb.AppendFormat("Message\t{0}", logDetails.Message).AppendLine();
            if (!string.IsNullOrEmpty(logDetails.MessageDetails))
            {
                sb.AppendFormat("Message Details\t{0}", logDetails.MessageDetails).AppendLine();
            }
            if (!string.IsNullOrEmpty(logDetails.ExceptionDetails))
            {
                sb.AppendFormat("Exception Details\t{0}", logDetails.ExceptionDetails).AppendLine();
            }
            sb.AppendLine();
            using (StreamWriter sw = new StreamWriter(logFilePath, true))
            {
                sw.WriteAsync(sb.ToString());
                sw.Flush();
            }
        }

        public RequesterDetails GetRequester(string CorrelationId, string Username, string Domain = "HEALTH")
        {
            return new RequesterDetails()
            {
                ApplicationId = Constants.APPLICATION_ID,
                CorrelationId = (string.IsNullOrEmpty(CorrelationId) ? Guid.NewGuid().ToString() : CorrelationId),
                MachineName = Environment.MachineName,
                ProcessName = Process.GetCurrentProcess().ProcessName,
                Domain = Domain,
                Username = Username
            };
        }
    }
}