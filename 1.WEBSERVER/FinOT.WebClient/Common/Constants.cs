using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAP.WebClient.Common
{
    public struct Constants 
    {
        public const int APPLICATION_ID = 26;
        public const int VIEWERROLE = 2600;
        public const string ENVIRONMENT = "ENVIRONMENT";
        public const string DEPLOYMENT_DATE = "DEPLOYMENTDATE";
        public const string FINPORTAL_URL = "FINANCEPORTALURL";
        public const string MYROLESPRIVILEGES_URL = "MYROLESPRIVILEGESURL";
        public const string LOGOUT_URL = "LOGOUTURL";
        public const string SERVICEDESK_URL = "SERVICEDESKURL";
        public const string RCAPIBASE_URL = "APIBASEURL";
        public const string APPHEADER_URL = "api/application/header/{0}";

        public const string FINAPISERVICE_ENDPOINT = "FINAPISERVICE_ENDPOINT";
        public const string FINLOGSERVICE_ENDPOINT = "FINLOGSERVICE_ENDPOINT";

        //Session Variables
        public const string SESSION_USERNAME = "USERNAME";
        public const string SESSION_APPMODEL = "APPMODEL_26";
        public const string SESSION_MENU = "MENU_26";
        public const string SESSION_REPORTS = "REPORTS_26";
        
        //Reports related
        public const string RPTUSER = "RPTUSER";
        public const string RPTPWD = "RPTPWD";
        public const string RPTDOMAIN = "RPTDOMAIN";
        public const string RPTUSERNAMEPARAM = "RPTUSERNAMEPARAM";
        public const string QUERYSTRING_RPTNAME = "ReportName";
        public const string QUERYSTRING_RPTPARAMS = "Parameters";
        public const string QUERYSTRING_RPTSERVER = "ReportServer";
        public const string QUERYSTRING_RPTPATH = "ReportPath";

        //Pagetags
        public const string WORKQUEUE_PAGETAG = "OTWORKQUEUE";
        public const string OTSEARCH_PAGETAG = "OTSEARCH";
    }
}