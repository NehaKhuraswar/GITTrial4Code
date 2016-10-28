using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using RAP.API.Providers;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(RAP.API.Startup))]
namespace RAP.API
{    
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            OAuthBearerOptions.Provider = new OAuthBearerAuthenticationProvider();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new OAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
#if DEBUG
                AllowInsecureHttp = true
#endif
            };

            //register OAuth server options
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            //register provider for OAuth Bearer Authentication
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
            app.UseWebApi(config);

        }
    }
}