using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Configuration;
using RAP.Core.DataModels;
using RAP.Core.Services;
using RAP.Core.Persisters;
using RAP.Business.Helper;
using RAP.DAL;
using RAP.Core.Common;

namespace RAP.Business.Implementation
{
    public class AccountManagementService : IAccountManagementService
    {
        public string CorrelationId { get; set; }
        private readonly IAccountManagementPersister persister;
        //public AccountManagementService(IAccountManagementPersister _persister)
        //{
        //    this.persister = _persister;
        //}

        //implements all methods from IMasterDataService

        AccountManagementDBHandler accDBHandler = new AccountManagementDBHandler();
        EmailService emailservice = new EmailService();
        ExceptionHandler _eHandler = new ExceptionHandler();

        public ReturnResult<CustomerInfo> SaveCustomer(CustomerInfo message)
        {
            ReturnResult<CustomerInfo> result = new ReturnResult<CustomerInfo>();
            string _loginURL = ConfigurationManager.AppSettings["loginURL"];
            bool bEdit = false;
            try
            {     
                if(message.custID !=0)
                {
                    bEdit = true;
                }
                var dbResult = accDBHandler.SaveCustomer(message);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                if (bEdit == false)
                {
                    EmailM emailMessage = new EmailM();
                    emailMessage.Subject = "RAP Account created Successfully";
                    emailMessage.MessageBody = NotificationMessage.ResourceManager.GetString("AccountCreatedMsg").Replace("PIN", dbResult.result.CustomerIdentityKey.ToString()).Replace("LOGIN", _loginURL).Replace("NAME", dbResult.result.User.FirstName + " " + dbResult.result.User.LastName);
                    emailMessage.RecipientAddress.Add(dbResult.result.email);
                    emailservice.SendEmail(emailMessage);
                }
                result.result = dbResult.result;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch(Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
               // _commonService.LogError(result.status);
                return result;
            }
        }

        public ReturnResult<bool> DeleteCustomer(CustomerInfo message)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            
            try
            {
                result = accDBHandler.DeleteCustomer(message);                
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                // _commonService.LogError(result.status);
                return result;
            }
        }

        public ReturnResult<bool> DeleteCityUser(int UserID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();

            try
            {
                result = accDBHandler.DeleteCityUser(UserID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                // _commonService.LogError(result.status);
                return result;
            }
        }

        public ReturnResult<ThirdPartyInfoM> GetThirdPartyInfo(int CustomerID)
        {
            return accDBHandler.GetThirdPartyInfo(CustomerID);
        }
        public ReturnResult<TranslationServiceInfoM> GetTranslationServiceInfo(int CustomerID)
        {
            return accDBHandler.GetTranslationServiceInfo(CustomerID);
        }
        public ReturnResult<bool> SaveOrUpdateThirdPartyInfo(RAP.Core.DataModels.ThirdPartyInfoM model)
        {
            return accDBHandler.SaveOrUpdateThirdPartyInfo(model);
        }
        public ReturnResult<bool> SaveTranslationServiceInfo(RAP.Core.DataModels.TranslationServiceInfoM model)
        {
            return accDBHandler.SaveTranslationServiceInfo(model);
        }   
        public ReturnResult<ThirdPartyInfoM> RemoveThirdPartyInfo(RAP.Core.DataModels.ThirdPartyInfoM model)
        {
            return accDBHandler.RemoveThirdPartyInfo(model);
        }
        public ReturnResult<CustomerInfo> EditCustomer(CustomerInfo message)
        {
            return accDBHandler.EditCustomer(message);
        }
        public ReturnResult<CityUserAccount_M> CreateCityUserAccount(CityUserAccount_M message)
        {          
           ReturnResult<CityUserAccount_M> result = new ReturnResult<CityUserAccount_M>();
           string _loginURL = ConfigurationManager.AppSettings["CityloginURL"];
            bool bEdit = false;
            try
            {
                if (message.UserID != 0)
                {
                    bEdit = true;
                }
                var dbResult = accDBHandler.CreateCityUserAccount(message);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                if (bEdit == false)
                {
                    EmailM emailMessage = new EmailM();
                    emailMessage.Subject = "RAP Account created Successfully";
                    emailMessage.MessageBody = NotificationMessage.ResourceManager.GetString("CityAccountCreatedMsg").Replace("LOGIN", _loginURL).Replace("NAME", dbResult.result.FirstName + " " + dbResult.result.LastName); 
                    if (dbResult.result.Email != null)
                    {
                        emailMessage.RecipientAddress.Add(dbResult.result.Email);
                    }
                    emailservice.SendEmail(emailMessage);
                }
                result.result = dbResult.result;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                // _commonService.LogError(result.status);
                return result;
            }
        }
        public ReturnResult<CustomerInfo> GetCustomer(CustomerInfo message)
        {
           return accDBHandler.GetCustomer(message);
        }
        public ReturnResult<CustomerInfo> GetCustomer(int custID)
        {
            return accDBHandler.GetCustomer(custID);
        }
        public ReturnResult<CustomerInfo> ChangePassword(CustomerInfo message)
        {
            return accDBHandler.ChangePassword(message);
        }

        public ReturnResult<bool> ForgetPwd(string email)
        {
            ReturnResult<string> result = new ReturnResult<string>();
            ReturnResult<bool> resultFinal = new ReturnResult<bool>();
            result = accDBHandler.ForgetPwd(email);
            if (result.status.Status == StatusEnum.Success)
            {
                EmailM emailMessage = new EmailM();
                emailMessage.Subject = "RAP Login Password";
                emailMessage.MessageBody = NotificationMessage.ResourceManager.GetString("ForgotPasswordMsg").Replace("PASSWORD", result.result);
                emailMessage.RecipientAddress.Add(email);
                EmailService emailservice = new EmailService();
                resultFinal = emailservice.SendEmail(emailMessage);
            }
            resultFinal.status = result.status;
            return resultFinal;
        }

        public ReturnResult<bool> ResendPin(CustomerInfo message)
        {
            ReturnResult<string> result = new ReturnResult<string>();
            ReturnResult<bool> resultFinal = new ReturnResult<bool>();
            result = accDBHandler.ResendPin(message);
            if(result != null)
            {
                EmailM emailMessage = new EmailM();
                emailMessage.Subject = "RAP Security PIN";
                emailMessage.MessageBody = NotificationMessage.ResourceManager.GetString("ResendPinMsg").Replace("PIN", result.result);
                if (message.email != null)
                {
                    emailMessage.RecipientAddress.Add(message.email);
                }
                EmailService emailservice = new EmailService();
                resultFinal = emailservice.SendEmail(emailMessage);              
            }
            return resultFinal;
        }
        public ReturnResult<CityUserAccount_M> GetCityUser(CityUserAccount_M message)
        {
            return accDBHandler.GetCityUser(message);
        }
        public ReturnResult<CityUserAccount_M> GetCityUserFromID(int CityUserID)
        {
            return accDBHandler.GetCityUserFromID(CityUserID);
        }
        public ReturnResult<CustomerInfo> SearchInviteCollaborator(String message)
        {
            return accDBHandler.SearchInviteCollaborator(message);
        }
        public ReturnResult<bool> AuthorizeCollaborator(CollaboratorAccessM access)
        {
            return accDBHandler.AuthorizeCollaborator(access);
        }
        public ReturnResult<bool> RemoveThirdParty(int CustID, int ThirdPartyRepresentationID)
        {
            return accDBHandler.RemoveThirdParty(CustID, ThirdPartyRepresentationID);
        }
        public ReturnResult<Collaborator> GetAuthorizedUsers(int custID)
        {
            return accDBHandler.GetAuthorizedUsers(custID);
        }

        public ReturnResult<List<AccountType>> GetAccountTypes(int AccountTypeID)
        {
            return accDBHandler.GetAccountTypes(AccountTypeID);
        }
        public ReturnResult<List<StateM>> GetStateList()
        {
            return accDBHandler.GetStateList();
        }
        public ReturnResult<SearchResult> GetAccountSearch(AccountSearch accountSearch)
        {
            return accDBHandler.GetAccountSearch(accountSearch);
        }

        
        
    }
}
