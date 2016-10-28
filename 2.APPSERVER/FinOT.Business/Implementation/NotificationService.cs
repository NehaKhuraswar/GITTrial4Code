using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using RAP.Core.Common;
using RAP.Core.Services;
using RAP.Core.Persisters;
using RAP.Core.DataModels;
using RAP.Business.Helper;

namespace RAP.Business.Implementation
{
    internal class NotificationService : INotificationService
    {
        public string CorrelationId { get; set; }
        private readonly INotificationPersister persister;
        public NotificationService(INotificationPersister _persister)
        {
            this.persister = _persister;
        }

        //implements all methods from ISearchService
    }
}
