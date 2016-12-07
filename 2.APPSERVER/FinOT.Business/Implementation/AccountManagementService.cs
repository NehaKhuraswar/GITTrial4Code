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
        public ReturnResult<CityUserAccount_M> CreateCityUserAccount(CityUserAccount_M message)
        {
            return accDBHandler.CreateCityUserAccount(message);
        }
        public ReturnResult<CustomerInfo> GetCustomer(CustomerInfo message)
        {
           return accDBHandler.GetCustomer(message);
        }
        public ReturnResult<CustomerInfo> SearchInviteThirdPartyUser(String message)
        {
            return accDBHandler.SearchInviteThirdPartyUser(message);
        }
        public ReturnResult<bool> AuthorizeThirdPartyUser(int CustID, int thirdpartyCustID)
        {
            return accDBHandler.AuthorizeThirdPartyUser(CustID, thirdpartyCustID);
        }
        public ReturnResult<bool> RemoveThirdParty(int CustID, int ThirdPartyRepresentationID)
        {
            return accDBHandler.RemoveThirdParty(CustID, ThirdPartyRepresentationID);
        }
        public ReturnResult<List<ThirdPartyDetails>> GetAuthorizedUsers(int custID)
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
