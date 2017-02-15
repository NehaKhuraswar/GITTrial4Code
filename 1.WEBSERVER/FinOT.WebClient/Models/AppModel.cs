using System;
using System.Web;
using System.Web.Configuration;
using RAP.WebClient.Common;

namespace RAP.WebClient.Models
{
    public class AppModel
    {
        public AppModel()
        {
            AppDetails = new AppDetails();

        }
      
        public AppDetails AppDetails { get; set; }

        //public string DefaultURL { get; set; }

    }

    public class AppDetails
    {
        public AppDetails()
        {
        
            ApiBaseURL = WebConfigurationManager.AppSettings[Constants.RAPAPIBASE_URL];
        }
        public string Environment { get; set; }
        public string DeploymentDate { get; set; }
        public string FinancePortalURL { get; set; }
        public string MyRolesPrivilegesURL { get; set; }
        public string LogoutURL { get; set; }
        public string ApiBaseURL { get; set; }
        public string DefaultURL { get; set; }
    }
}