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


namespace RAP.Business.Implementation
{
    internal class AccountManagementService : IAccountManagementService
    {
        public string CorrelationId { get; set; }
        private readonly IAccountManagementPersister persister;
        public AccountManagementService(IAccountManagementPersister _persister)
        {
            this.persister = _persister;
        }

        //implements all methods from IMasterDataService
    }
}
