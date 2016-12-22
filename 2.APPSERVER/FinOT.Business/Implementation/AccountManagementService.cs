using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
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

        public ReturnResult<CustomerInfo> SaveCustomer(CustomerInfo message)
        {
            return accDBHandler.SaveCustomer(message);
        }
        public ReturnResult<CustomerInfo> EditCustomer(CustomerInfo message)
        {
            return accDBHandler.EditCustomer(message);
        }
        public ReturnResult<CityUserAccount_M> CreateCityUserAccount(CityUserAccount_M message)
        {
            return accDBHandler.CreateCityUserAccount(message);
        }
        public ReturnResult<CustomerInfo> GetCustomer(CustomerInfo message)
        {
           return accDBHandler.GetCustomer(message);
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
            if (result != null)
            {
                EmailM emailMessage = new EmailM();
                emailMessage.MessageBody = email + " Sending Password " + result.result;
                emailMessage.Subject = "Your RAP Password";
                EmailService emailservice = new EmailService();
                resultFinal = emailservice.SendEmail(emailMessage);
            }
            return resultFinal;
        }

        public ReturnResult<bool> ResendPin(CustomerInfo message)
        {
            ReturnResult<Int32> result = new ReturnResult<Int32>();
            ReturnResult<bool> resultFinal = new ReturnResult<bool>();
            result = accDBHandler.ResendPin(message);
            if(result != null)
            {
                EmailM emailMessage = new EmailM();
                emailMessage.MessageBody = message.email + " Sending Pin " + result.result;
                emailMessage.Subject = "Your RAP Pin";
                EmailService emailservice = new EmailService();
                resultFinal = emailservice.SendEmail(emailMessage);
            }
            return resultFinal;
        }
        public ReturnResult<CityUserAccount_M> GetCityUser(CityUserAccount_M message)
        {
            return accDBHandler.GetCityUser(message);
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

        public ReturnResult<List<AccountType>> GetAccountTypes()
        {
            return accDBHandler.GetAccountTypes();
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
