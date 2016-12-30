using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using RAP.Core.Common;
using RAP.Core.DataModels;
using RAP.Core.Services;
using System.Configuration;


namespace RAP.Business.Implementation
{
    public class EmailService : IEmailService
    {
        public ReturnResult<bool> SendEmail(EmailM message)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                string hostServer = ConfigurationManager.AppSettings["HostServer"];
                int portNumber = Convert.ToInt32(ConfigurationManager.AppSettings["PortNumber"]);               
                string senderAddress = ConfigurationManager.AppSettings["SenderAddress"];
                string password = ConfigurationManager.AppSettings["SenderPassword"];
                string bcc = ConfigurationManager.AppSettings["BCC"];
                bool enableSSL = string.IsNullOrEmpty(ConfigurationManager.AppSettings["EnableSSL"]) ? false : (ConfigurationManager.AppSettings["EnableSSL"] == "true" ? true : false);
                bool defaultAuthentication = string.IsNullOrEmpty(ConfigurationManager.AppSettings["DefaultAuthentication"]) ? false : (ConfigurationManager.AppSettings["DefaultAuthentication"] == "true" ? true : false);
                bool includeBCC = string.IsNullOrEmpty(ConfigurationManager.AppSettings["IncludeBCC"]) ? false : (ConfigurationManager.AppSettings["IncludeBCC"] == "true" ? true : false);
                
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(senderAddress);
                    mail.To.Add("venky.soundar@gcomsoft.com,neha.bhandari@gcomsoft.com,sanjay@gcomsoft.com");
                    mail.Subject = message.Subject;
                    mail.Body = message.MessageBody;
                    if (includeBCC)
                    {
                        mail.Bcc.Add(bcc);
                    }          
                    mail.IsBodyHtml = false;
                    //if (message.Attachments != null)
                    //{
                    //    foreach (string attahment in message.Attachments)
                    //    {
                    //        mail.Attachments.Add(new Attachment(attahment));
                    //    }
                    //}

                    using (SmtpClient smtp = new SmtpClient(hostServer, portNumber))
                    {
                        if(defaultAuthentication)
                        {
                            smtp.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                        }
                        else
                        {
                            smtp.Credentials = new NetworkCredential(senderAddress.Trim(), password.Trim());
                        }
                        
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
                    result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
    }
}
