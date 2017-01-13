using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mime;
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
               // if (message.RecipientAddress != null && message.RecipientAddress.Any())
                //{
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(senderAddress);
                        mail.To.Add("RAP.Oakland@gmail.com");
                        if (message.RecipientAddress != null && message.RecipientAddress.Any())
                        {
                           mail.To.Add(String.Join(",",message.RecipientAddress));
                        }                      
                        mail.Subject = message.Subject;
                        mail.Body = message.MessageBody;
                        if (includeBCC)
                        {
                            mail.Bcc.Add(bcc);
                        }
                        mail.IsBodyHtml = false;
                        if (message.Attachments != null && message.Attachments.Any())
                        {
                            foreach (var attahment in message.Attachments)
                            {
                                var byteArray = Convert.FromBase64String(attahment.Base64Content);
                                MemoryStream ms = new MemoryStream(byteArray);
                                ContentType ct = new ContentType(System.Net.Mime.MediaTypeNames.Text.Plain);
                                Attachment at = new Attachment(ms, ct);
                                at.ContentDisposition.FileName = attahment.DocName;
                                mail.Attachments.Add(at);              

                            }
                        }

                        using (SmtpClient smtp = new SmtpClient(hostServer, portNumber))
                        {
                            if (defaultAuthentication)
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
                    result.result = true;
                    result.status = new OperationStatus() { Status = StatusEnum.Success };
                //}
                //else
                //{
                //    throw new Exception("Email Recipient not found");
                //}
                    
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
