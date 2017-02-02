using System;
using System.Web;

using RAP.Core.FinServices.APIService;
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
        public ApplicationHeader Header { get; set; }

        public AppDetails AppDetails { get; set; }

        //public string DefaultURL { get; set; }

    }

    public class AppDetails
    {
        public AppDetails()
        {
            //Environment = WebConfigurationManager.AppSettings[Constants.ENVIRONMENT];
            //DeploymentDate = WebConfigurationManager.AppSettings[Constants.DEPLOYMENT_DATE];
            //FinancePortalURL = WebConfigurationManager.AppSettings[Constants.FINPORTAL_URL];
            //MyRolesPrivilegesURL = WebConfigurationManager.AppSettings[Constants.MYROLESPRIVILEGES_URL];
            //LogoutURL = WebConfigurationManager.AppSettings[Constants.LOGOUT_URL];
            ApiBaseURL = WebConfigurationManager.AppSettings[Constants.RCAPIBASE_URL];
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