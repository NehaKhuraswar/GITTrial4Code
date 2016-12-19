using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;

namespace RAP.WebClient
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebAPIConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //string username = HttpContext.Current.User.Identity.Name;
            //string domain = username.Contains(@"\") ? username.Substring(0, username.IndexOf(@"\")) : null;
            //username = username.Contains(@"\") ? username.Substring(username.LastIndexOf(@"\") + 1) : username;
            //username = username.ToLower();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.ApplicationPath != "/"
                    && Request.ApplicationPath.Equals(Request.Path, StringComparison.CurrentCultureIgnoreCase))
            {
                var redirectUrl = VirtualPathUtility.AppendTrailingSlash(Request.ApplicationPath);
                Response.RedirectPermanent(redirectUrl);
            }

            if (!(HttpContext.Current.Request.IsSecureConnection))
            {
#if RELEASE
                Response.Redirect(Request.Url.ToString().Replace("http://", "https://"));
#endif
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}