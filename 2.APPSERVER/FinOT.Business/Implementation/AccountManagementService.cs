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

        public bool SaveCustomer(CustomerInfo message)
        {
            return accDBHandler.SaveCustomer(message);
        }
        public CustomerInfo GetCustomer(CustomerInfo message)
        {
            return accDBHandler.GetCustomer(message);
        }
    }
}
