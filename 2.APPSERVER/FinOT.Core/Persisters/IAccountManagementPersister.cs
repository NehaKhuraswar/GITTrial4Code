using System;
using System.Collections.Generic;
using RAP.Core.DataModels;
using System.Data;

namespace RAP.Core.Persisters
{
    public interface IAccountManagementPersister
    {
        string CorrelationId { get; set; }
        
    }
}
