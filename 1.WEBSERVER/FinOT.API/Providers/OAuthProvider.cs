using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using RAP.Core.FinServices.APIService;
using RAP.API.Common;
using RAP.Core.DataModels;
using RAP.Business.Implementation;
using RAP.Core.Common;

namespace RAP.API.Providers
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            string Username = string.Empty, Roles = string.Empty, CorrelationId = Guid.NewGuid().ToString();
            Username = context.UserName;
            //using owincontext
            if (string.IsNullOrEmpty(Username))
            {
                Username = HttpContext.Current.GetOwinContext().Request.User.Identity.Name;
            }

            CustomerInfo cust = new CustomerInfo();
            cust.email = Username;
            cust.Password = context.Password;

            AccountManagementService accService = new AccountManagementService();
            ReturnResult<CustomerInfo> result = new ReturnResult<CustomerInfo>();
            result = accService.GetCustomer(cust);
            if (result.status.Status != StatusEnum.Success)
            {

                return;
            }

            
            //using windowsidentity
            //if (string.IsNullOrEmpty(Username))
            //{
            //    Username = WindowsIdentity.GetCurrent().Name;
            //}
            ////extract the username excluding domain name
            //Username = Username.Contains(@"\") ? Username.Substring(Username.LastIndexOf(@"\") + 1) : Username;
            //Username = Username.ToLower();

            ////hardcoded values for test
            //List<Role> roles = new List<Role>();
            //roles.Add(new Role() { Id = 2400, Name = "Viewer", Description = "Viewer" });
            //roles.Add(new Role() { Id = 2401, Name = "Originator", Description = "Originator" });
            //Roles = string.Join(",", from item in roles select item.Id.ToString());

            //check if the user has access
            APIHelper api = new APIHelper();
            //IList<int> roles = api.GetUserRoles(Username, CorrelationId);
            IList<int> roles = new List<int>();
            roles.Add(2600);

            
            
            if (roles != null)
            {
                if (roles.Count > 0)
                {
                    Roles = string.Join(",", roles);
                }
            }

            if (string.IsNullOrEmpty(Roles))
            {
                context.SetError("NOACCESS");
                return;
            }

            //if user has access generate token with Username and Roles in claims
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, Username));
            //identity.AddClaim(new Claim(ClaimTypes.Role, Roles));
            identity.AddClaim(new Claim(ClaimTypes.SerialNumber, CorrelationId));
           // identity.AddClaim(new Claim(ClaimTypes.GivenName, result.result.User.FirstName));
            AuthenticationProperties properties = CreateProperties(Username, result.result.User.FirstName);

            AuthenticationTicket ticket = new AuthenticationTicket(identity, properties);
            await Task.Run(() => context.Validated(ticket));
        }

        public static AuthenticationProperties CreateProperties(string Username,  string FirstName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "Username", Username },
               
                {"FirstName", FirstName}
            };
            return new AuthenticationProperties(data);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }


}