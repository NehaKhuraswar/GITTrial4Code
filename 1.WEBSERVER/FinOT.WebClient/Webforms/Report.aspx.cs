using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using RAP.WebClient.Common;

namespace RAP.WebClient.Webforms
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string UserName = GetUserName();
                    //getting the request parameters to pass to report viewer
                    string rptName = Request.QueryString[Constants.QUERYSTRING_RPTNAME];
                    string reportParameter = Request.QueryString[Constants.QUERYSTRING_RPTPARAMS];

                    //getting report server credentials from appsettings
                    string rptUser = System.Configuration.ConfigurationManager.AppSettings[Constants.RPTUSER];
                    string rptPwd = System.Configuration.ConfigurationManager.AppSettings[Constants.RPTPWD];
                    string rptDomain = System.Configuration.ConfigurationManager.AppSettings[Constants.RPTDOMAIN];
                    string rptUserNameParam = System.Configuration.ConfigurationManager.AppSettings[Constants.RPTUSERNAMEPARAM];

                    ReportParameter[] _params = null;
                    if (!string.IsNullOrEmpty(rptName))
                    {
                        Page.Title = rptName;
                        //setting report server and path detail
                        rptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                        rptViewer.ServerReport.ReportServerUrl = new Uri(Request.QueryString[Constants.QUERYSTRING_RPTSERVER]);
                        rptViewer.ServerReport.ReportPath = Request.QueryString[Constants.QUERYSTRING_RPTPATH].ToString() + rptName;
                        rptViewer.ServerReport.ReportServerCredentials = new ReportCredential(rptUser, rptPwd, rptDomain);

                        //Setting report parameters
                        reportParameter += ((string.IsNullOrEmpty(reportParameter) ? "" : ",") + rptUserNameParam);
                        if (!string.IsNullOrEmpty(reportParameter))
                        {
                            String[] parameterNames = reportParameter.Split(',');
                            _params = new ReportParameter[parameterNames.Length];
                            for (int i = 0; i < parameterNames.Length; i++)
                            {
                                if (parameterNames[i] == rptUserNameParam)
                                {
                                    _params[i] = new ReportParameter(rptUserNameParam, UserName, false);
                                }
                                else
                                {
                                    _params[i] = new ReportParameter(parameterNames[i], Request.QueryString[parameterNames[i]], Convert.ToBoolean(Request.QueryString["Show" + parameterNames[i]]));
                                }
                            }
                        }

                        if (_params != null)
                            rptViewer.ServerReport.SetParameters(_params);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private string GetUserName()
        {
            if (User != null)
            {
                return User.Identity.Name.Split('\\')[1];
            }
            return string.Empty;
        }
    }
}