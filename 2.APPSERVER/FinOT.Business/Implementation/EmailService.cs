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
                string hostAddress = ConfigurationManager.AppSettings["HostServer"];
                int portNumber = Convert.ToInt32(ConfigurationManager.AppSettings["PortNumber"]);
                bool enableSSL = string.IsNullOrEmpty(ConfigurationManager.AppSettings["EnableSSL"]) ? true : (ConfigurationManager.AppSettings["EnableSSL"] == "true" ? true : false);
                string emailFrom = ConfigurationManager.AppSettings["SenderAddress"];
                string password = ConfigurationManager.AppSettings["SenderPassword"];
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add("venky.soundar@gcomsoft.com,neha.bhandari@gcomsoft.com,sanjay@gcomsoft.com");
                    mail.Subject = message.Subject;
                    mail.Body = message.MessageBody;
                    //mail.CC.Add(string.Join(",", message.CC.Select(a => string.Concat("'", a, "'"))));
                    //mail.Bcc.Add(string.Join(",", message.BCC.Select(a => string.Concat("'", a, "'"))));
                    mail.IsBodyHtml = false;
                    if (message.Attachments != null)
                    {
                        foreach (string attahment in message.Attachments)
                        {
                            mail.Attachments.Add(new Attachment(attahment));
                        }
                    }

                    using (SmtpClient smtp = new SmtpClient(hostAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom.Trim(), password.Trim());
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
