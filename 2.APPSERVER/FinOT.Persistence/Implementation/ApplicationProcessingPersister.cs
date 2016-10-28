using System;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using RAP.Core.DataModels;
using RAP.Core.Persisters;
using RAP.Persistence.ADO;


namespace RAP.Persistence.Implementation
{
    internal class ApplicationProcessingPersister : IApplicationProcessingPersister
    {
        public string CorrelationId { get; set; }
        Database _db;
        public ApplicationProcessingPersister(Database db, string connectionName)
        {
            this._db = db;
            _db.CreateConnection(connectionName);
        }

        //implements all methods from ISearchPersister
    }
}
