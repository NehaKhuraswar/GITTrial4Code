using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Core.DataModels
{
    
    public class CustomerInfo
    {
        public CustomerInfo()
        {
            thirdpartyDetails = new List<ThirdPartyDetails>();
            User = new UserInfoM();
        }
        public string AccountType { get; set; }
        public int selected { get; set; }
        public int custID { get; set; }
        public string email { get; set; }
        public string Password { get; set; }
        public bool EmailNotificationFlag { get; set; }
        public bool MailNotificationFlag { get; set; }
        public List<ThirdPartyDetails> thirdpartyDetails { get; set; }
        public UserInfoM User { get; set; }
        public Int32 CustomerIdentityKey { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class CityUserAccount_M
    {
        public AccountType AccountType { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int EmployeeID { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string OfficeLocation { get; set; }
        public string OfficePhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public bool IsHearingOfficer { get; set; }
        public bool IsAnalyst { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class Rent
    {

        public int id { get; set; }
        public string name { get; set; }
    }
    public class ThirdPartyDetails
    {
        public int ThirdPartyRepresentationID { get; set; }
        public int custID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
    }
    public class PetitionDetails
    {
        public int PetitionID { get; set; }
        public string Desc { get; set; }

    }
    public class LoginInfo
    {
        public string email { get; set; }
        public string Password { get; set; }

    }

    public class SearchResult
    {
        public SearchResult()
        {
            List = new List<SearchResultCustomerInfo>();
        }
        public List<SearchResultCustomerInfo> List;
        public string SortBy;
        public bool SortReverse;
        public int PageSize;
        public int CurrentPage;
        public int TotalCount;
    }

    public class SearchResultCustomerInfo
    {
        public string AccountType { get; set; }       
        public int custID { get; set; }
        public string email { get; set; }
        public string Name { get; set; }
        public int RankNo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
