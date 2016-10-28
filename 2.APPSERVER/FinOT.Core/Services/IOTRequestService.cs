using RAP.Core.DataModels;
using System.Collections.Generic;

namespace RAP.Core.Services
{
    public interface IOTRequestService
    {
        string CorrelationId { get; set; }
        
        OTRequest GetOTRequest(int ReqID, int? FY, string Username);

        List<Notes> GetNotes(int ReqID, string Username);
             
        int SaveOTRequest(int? ReqID, Header Header, string Username, out IList<string> Warnings);

        bool SaveNotes(int ReqID, Notes objNote, string Username);
    }
}
