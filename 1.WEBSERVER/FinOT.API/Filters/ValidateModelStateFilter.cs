using System;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using RAP.API.Common;
using RAP.API.Models;

namespace RAP.API
{    
    public class ValidateModelStateAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            if (!actionContext.ModelState.IsValid)
            {
                TranInfo<Dictionary<string, string[]>> transactionInformation = new TranInfo<Dictionary<string, string[]>>();
                transactionInformation.errors = actionContext.ModelState.Errors();
                transactionInformation.status = false;
                actionContext.Response = actionContext.Request.CreateResponse<TranInfo<Dictionary<string, string[]>>>(HttpStatusCode.BadRequest, transactionInformation);
            }

        }
    }
}