using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Security.Claims;
using RAP.Core.Services;
using RAP.Core.DataModels;
using RAP.API.Models;
using RAP.API.Common;

namespace RAP.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/applicationprocessing")]
    public class ApplicationProcessingController : ApiController
    {
        private string Username, ExceptionMessage, InnerExceptionMessage;
        private readonly IApplicationProcessingService service;
        public ApplicationProcessingController() { }
        public ApplicationProcessingController(IApplicationProcessingService _service)
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
        [HttpGet]
        [Route("getRent")]
        public HttpResponseMessage GetRent()
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<Rent>> transaction = new TranInfo<List<Rent>>();

            try
            {
                //  ExtractClaimDetails();

                List<Rent> obj;
                //if (custid.HasValue)
                //{
                //  //  obj = service.GetCustomer((int)reqid, fy, Username);
                //}
                //else
                //{
                obj = new List<Rent>();
                //}
                Rent obj1 = new Rent() { id = 1, name = "Yes" };
                Rent obj2 = new Rent() { id = 2, name = "No" };
                obj.Add(obj1);
                obj.Add(obj2);
                transaction.data = obj;
                transaction.status = true;
            }
            catch (Exception ex)
            {
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;

                if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //  LogHelper.Instance.Error(service.CorrelationId, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<List<Rent>>>(ReturnCode, transaction);
        }

        [Route("getcaseinfo")]
        [HttpGet]
        public HttpResponseMessage GetCaseInfo()
        {
            //Appl accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();

            try
            {

                CaseInfoM obj;
                obj = new CaseInfoM();
              //  obj = accService.GetAuthorizedUsers((int)custID);
                //if (obj != null)
                //{
                        transaction.data = obj;
                    transaction.status = true;
                //}
                //else
                //{
                //    transaction.data = null;
                //    transaction.status = false;
                //}


            }
            catch (Exception ex)
            {
                // transaction.AddException(ex.Message);
                //ReturnCode = HttpStatusCode.InternalServerError;

                //if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //LogHelper.Instance.Error(CorrelationID, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }
        #endregion

        #region "POST REQUEST"
        [Route("savecaseinfo")]
        [HttpPost]
        public HttpResponseMessage SaveCaseInfo([FromBody] CaseInfoM caseModel)
        {
            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();

            try
            {
           //     transaction.status = accService.SaveCaseInfo(caseModel);
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }
        //all POST requests goes here
        #endregion

    }
}
