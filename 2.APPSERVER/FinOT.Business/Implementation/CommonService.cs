using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using RAP.Core.Common;
using RAP.Core.Services;
using RAP.Core.DataModels;
using RAP.Business.Helper;
using RAP.DAL;

namespace RAP.Business.Implementation
{
    public class CommonService : ICommonService
    {
        public string CorrelationId { get; set; }
        private readonly ICommonDBHandler _dbHandler;
        private readonly IExceptionHandler _eHandler;
    
        //TBD
        //public CommonService()
        //{
        //    _dbHandler = new CommonDBHandler();            
        //}
        public CommonService(ICommonDBHandler dbHandler, IExceptionHandler eHandler)
        {
            this._dbHandler = dbHandler;
            this._eHandler = eHandler;
        }
        public void LogError(OperationStatus status)
        {
            try
            {
                _dbHandler.SaveErrorLog(status);
            }
            catch(Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                eHandler.HandleException(ex);               
            }     
                        
        }

       public ReturnResult<DocumentM> SaveDocument(DocumentM doc)
        {
            ReturnResult<DocumentM> result = new ReturnResult<DocumentM>();
             try
             {
                 result = _dbHandler.SaveDocument(doc);
                 return result;
             }
             catch (Exception ex)
             {
                 result.status = _eHandler.HandleException(ex);
                 LogError(result.status);
                 return result;
             }
        }

       public ReturnResult<List<DocumentM>> GetDocuments(int CustmerID, bool isPetitiofiled, string docTitle = null)
       {
           ReturnResult<List<DocumentM>> result = new ReturnResult<List<DocumentM>>();
           try
           {
               result = _dbHandler.GetDocuments(CustmerID, isPetitiofiled, docTitle);
               return result;
           }
           catch (Exception ex)
           {
               result.status = _eHandler.HandleException(ex);
               LogError(result.status);
               return result;
           }
       }
       public ReturnResult<APNAddress> UpdateAPNAddress(APNAddress apnAddress)
       {
           ReturnResult<APNAddress> result = new ReturnResult<APNAddress>();
           try
           {
               result = _dbHandler.UpdateAPNAddress(apnAddress);
               return result;
           }
           catch (Exception ex)
           {
               result.status = _eHandler.HandleException(ex);
               LogError(result.status);
               return result;
           }
       }
      public ReturnResult<List<DocumentM>> GetCaseDocuments(int c_id)
       {
           ReturnResult<List<DocumentM>> result = new ReturnResult<List<DocumentM>>();
           try
           {
               result = _dbHandler.GetCaseDocuments(c_id);
               return result;
           }
           catch (Exception ex)
           {
               result.status = _eHandler.HandleException(ex);
               LogError(result.status);
               return result;
           }
       }

      public ReturnResult<DocumentM> SaveCaseDocument(DocumentM doc)
        {
            ReturnResult<DocumentM> result = new ReturnResult<DocumentM>();
            try
            {
                result = _dbHandler.SaveCaseDocument(doc);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                LogError(result.status);
                return result;
            }
        }

       public ReturnResult<List<string>> GetDocDescription()
       {
           ReturnResult<List<string>> result = new ReturnResult<List<string>>();
           try
           {
               result = _dbHandler.GetDocDescription();
               return result;
           }
           catch (Exception ex)
           {
               result.status = _eHandler.HandleException(ex);
               LogError(result.status);
               return result;
           }
       }
       public ReturnResult<CustomEmailM> GetCustomEmailNotification(int c_id, int ActivityID, int NotificationID)
       {
           ReturnResult<CustomEmailM> result = new ReturnResult<CustomEmailM>();
           try
           {
               result = _dbHandler.GetCustomEmailNotification(c_id, ActivityID, NotificationID);
               return result;
           }
           catch (Exception ex)
           {
               result.status = _eHandler.HandleException(ex);
               LogError(result.status);
               return result;
           }
       }
       public ReturnResult<EmailM> SaveCustomEmailNotification(EmailM message, int cityUserID, int c_id, int activityID)
       {
           ReturnResult<EmailM> result = new ReturnResult<EmailM>();
           try
           {
               result = _dbHandler.SaveCustomEmailNotification(message, cityUserID, c_id, activityID);
               return result;
           }
           catch (Exception ex)
           {
               result.status = _eHandler.HandleException(ex);
               LogError(result.status);
               return result;
           }
       }

       public ReturnResult<MailM> SaveMailNotification(MailM message)
       {
           ReturnResult<MailM> result = new ReturnResult<MailM>();
           try
           {
               result = _dbHandler.SaveMailNotification(message);
               return result;
           }
           catch (Exception ex)
           {
               result.status = _eHandler.HandleException(ex);
               LogError(result.status);
               return result;
           }
       }
     public ReturnResult<bool> MailSentActivity(int C_ID, int SentBy, int ActivityID, int NotificationID)
       {
           ReturnResult<bool> result = new ReturnResult<bool>();
           try
           {
               result = _dbHandler.MailSentActivity(C_ID, SentBy, ActivityID, NotificationID);
               return result;
           }
           catch (Exception ex)
           {
               result.status = _eHandler.HandleException(ex);
               LogError(result.status);
               return result;
           }
       }
        
    }
}
