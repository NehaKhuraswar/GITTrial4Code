using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RAP.Core.DataModels
{

    public class Dashboard_M
    {
        private List<Cases> _cases = new List<Cases>();
        public List<Cases> cases
        {
            get
            {
                return _cases;
            }
            set
            {
                _cases = value;
            }
        }
    }
    public class Cases
    {
        public int C_ID { get; set; }
        private List<ActivityStatus_M> _activityStatus = new List<ActivityStatus_M>();

        public List<ActivityStatus_M> ActivityStatus
        {
            get
            {
                return _activityStatus;
            }
            set
            {
                _activityStatus = value;
            }
        }
    }
    public class ActivityStatus_M
    {
        public ActivityStatus_M()
        {
            Activity = new Activity_M();
            Status = new Status_M();
            Date = null;
            
        }
        public Activity_M Activity { get; set; }
        public Status_M Status { get; set; }
        public DateTime? Date { get; set; }
        public string CreatedBy { get; set; }
        public string Notes { get; set; }
        public int EmployeeID { get; set; }
    }
    public class Activity_M
    {
        public int ActivityID { get; set; }
        public string ActivityDesc { get; set; }
    }
    public class Status_M
    {
        public int StatusID { get; set; }
        public string StatusDesc { get; set; }
    }

    public class AccountSearch
    {
        public AccountSearch()
        {
            //FromDate = new DateTime(2016, 10 , 28);
            //ToDate = DateTime.Now;
            FromDate = null;
            ToDate = null;
        }

        public AccountType AccountType { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? APNNumber { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageSize { get; set; }
        public string SortBy {get; set;} 
		public bool SortReverse {get; set;}
        public int CurrentPage { get; set; }
    }
    public class CaseSearch
    {
        public CaseSearch()
        {
            Analyst = new CityUserAccount_M();
            HearingOfficer = new CityUserAccount_M();
            FromDate = null;
            ToDate = null;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? APNNumber { get; set; }
        public CityUserAccount_M Analyst { get; set; }
        public CityUserAccount_M HearingOfficer { get; set; }
        public int AnalystID { get; set; }
        public int HearingOfficerID { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string CaseNumber { get; set; }
        public int CaseStatus { get; set; }
       
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public bool SortReverse { get; set; }
        public int CurrentPage { get; set; }
    }
    public class AccountType
    {
        public int AccountTypeID { get; set; }
        public string AccountTypeDesc { get; set; }
    }
    public class CustomEmailM
    {
        public CustomEmailM()
        {
            Recipients = new List<string>();
            Message = new EmailM();
        }
        public List<string> Recipients { get; set; }
        public EmailM Message { get; set; }
        public int EmployeeID { get; set; }
        public int C_ID { get; set; }
    }
    //public class CaseStatusActivity_M
    //{
    //    public int ActivityID { get; set; }
    //    public string ActivityName { get; set; }
    //    public int StatusID { get; set; }
    //    public string StatusDesc { get; set; }
    //}
    

  
}
