    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAP.Core.Common;

namespace RAP.Core.DataModels
{
    public class OTRequest
    {
        public OTRequest()
        {
            CustDetails = new CustDetails();
            Header = new Header();
            Staff = new Staff();
            Funding = new Funding();
            Approvals = new Approvals();          
            
        }
        public int? ReqID { get; set; }
        public CustDetails CustDetails { get; set; }
        public Header Header { get; set; }
        public Staff Staff { get; set; }
        public Funding Funding { get; set; }
        public Approvals Approvals { get; set; }
        
    }
    public class CustDetails
    {
        public CustDetails()
        {
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class Header
    {
        public Header()
        {
        RequestType = new IDDescription()
            {
                ID = (int)Common.RequestType.Regular,
                Description = Enum.GetName(typeof(RequestType), Common.RequestType.Regular)
            };
            StartDate = DateTime.Today;
            EndDate = DateTime.Today.AddDays(90);
        }
        public string OTCode { get; set; }
        public IDDescription RequestType { get; set; }
        public IDDescription Status { get; set; }
        public IDDescription Workflow { get; set; }        
        public string BriefDescription {get; set;}
        public string DetailDescription {get; set;}
        public string CashOrComp { get; set; }
        public string BureauOwner {get; set;}
        public DateTime  StartDate { get; set; }
        public DateTime EndDate{get; set;}
        public int IFY { get; set; }
        public decimal AuthorizedOTAmount{get;set;}
        public decimal EstimatedOTHours{get;set;}
        public decimal AuthorizedOTHours{get;set;}
        public bool ActiveOTCode {get; set;}        

    }
    public class Staff
    {
        public Staff()
        {
            
        }
        public string EDBBureau{get;set;}
        public string FirstName{get;set;}
        public string LastName{get;set;}
        public int ERN{get; set;}
        public string TitleCode {get; set;}
        public string Title{get; set;}
        public string WorkUnit{get; set;}
        public string Supervisor{get;set;}
    }

    public class Funding
    {
        public Funding()
        {
        }
    }

    public class Approvals
    {
        public Approvals()
        {
        }
    }
    public class Notes
    {
        public int ItemID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Context { get; set; }
        public string NoteDescription { get; set; }
        public string CreatedBy { get; set; }

    }

}
