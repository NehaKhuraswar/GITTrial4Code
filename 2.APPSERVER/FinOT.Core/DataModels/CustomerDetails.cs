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
        public int selected { get; set; }
        public int custID { get; set; }
        public string email { get; set; }
        public string Password { get; set; }
        public bool EmailNotificationFlag { get; set; }
        public bool MailNotificationFlag { get; set; }
        public List<ThirdPartyDetails> thirdpartyDetails { get; set; }
        public UserInfoM User { get; set; }
 
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
}
