using System;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Linq;

namespace RAP.API
{
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    //public class AuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    //{
    //    public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
    //    {
    //        base.OnAuthorization(actionContext);
    //    }

    //    protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
    //    {
    //        if (actionContext.RequestContext.Principal.Identity.IsAuthenticated)
    //        {
    //            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
    //        }
    //        else
    //        {
    //            base.HandleUnauthorizedRequest(actionContext);
    //        }
    //    }
    //}
}