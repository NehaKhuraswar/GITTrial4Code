using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace RAP.API
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //register unity from configuration
            UnityConfig.Register();
            //register WebApi and related configuration
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}