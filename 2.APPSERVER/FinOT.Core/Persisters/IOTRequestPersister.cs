using System;
using System.Collections.Generic;
using System.Data;

namespace RAP.Core.Persisters
{
    public interface IOTRequestPersister
    {
        string CorrelationId { get; set; }

        DataSet GetOTRequest(int ReqID, int? FY, string Username);

        DataTable SaveOTRequest(ref int? ReqID, string OTCode, int RequestTypeID, string BriefDescription,
            string DetailDescription, string CashOrComp, string BureauOwner, DateTime StartDate, DateTime EndDate, Decimal AuthorizedOTAmount,
            Decimal EstimatedOTHours, Decimal AuthorizedOTHours, bool ActiveOTCode, string Username);

        bool SaveNotes(int ReqID, string NoteDescription, string Context, string CreatedBy);        

        DataTable GetNotes(int ReqID, string Username);

        
    }
}
