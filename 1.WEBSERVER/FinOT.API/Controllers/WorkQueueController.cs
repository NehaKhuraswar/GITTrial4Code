using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Security.Claims;

using RAP.Core.Services;
using RAP.API.Models;
using RAP.API.Common;
using RAP.Core.DataModels;
using RAP.Core.Common;


namespace RAP.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/workqueue")]
    public class WorkQueueController : ApiController
    {
        private string Username, ExceptionMessage, InnerExceptionMessage;
        private readonly INotificationService service;
        public WorkQueueController(INotificationService _service)
        {
            service = _service;
        }

        public void ExtractClaimDetails()
        {
            HttpRequestContext context = Request.GetRequestContext();
            var principle = Request.GetRequestContext().Principal as ClaimsPrincipal;
            service.CorrelationId = principle.Claims.Where(x => x.Type == ClaimTypes.SerialNumber).FirstOrDefault().Value;
            Username = principle.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
            ExceptionMessage = "An error occured while processing your request. Reference# " + service.CorrelationId;
        }

        #region "GET REQUESTS"
        //all GET requests goes here
        #endregion

        #region "POST REQUEST"
        //all POST requests goes here
        #endregion
    }
}
