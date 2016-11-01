using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using RAP.API.Models;
using RAP.API.Common;
using RAP.Core.DataModels;
using RAP.Core.Common;
using RAP.Core.Services;

namespace RAP.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/otrequest")]
    public class OTRequestController : ApiController
    {
        private string Username, ExceptionMessage, InnerExceptionMessage;
        private readonly IOTRequestService service;
        public OTRequestController(IOTRequestService _service)
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
        [Route("get/{reqid:int?}/{fy:int?}")]
        public HttpResponseMessage GetOTRequest(int? reqid = null, int? fy = null)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<OTRequest> transaction = new TranInfo<OTRequest>();

            try
            {
                ExtractClaimDetails();

                OTRequest obj;
                if (reqid.HasValue)
                {
                    obj = service.GetOTRequest((int)reqid, fy, Username);
                }
                else
                {
                    obj = new OTRequest();
                }

                transaction.data = obj;
                transaction.status = true;
            }
            catch (Exception ex)
            {
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;

                if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                LogHelper.Instance.Error(service.CorrelationId, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<OTRequest>>(ReturnCode, transaction);
        }

        [HttpGet]
        [Route("notes/get/{ReqID:int}")]
        public HttpResponseMessage GetNotes(int ReqID)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<Notes>> transaction = new TranInfo<List<Notes>>();

            try
            {
                ExtractClaimDetails();

                transaction.data = service.GetNotes(ReqID, Username);
                transaction.status = true;
            }
            catch (Exception ex)
            {
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;

                if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                LogHelper.Instance.Error(service.CorrelationId, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<List<Notes>>>(ReturnCode, transaction);
        }
        //all GET requests goes here
        #endregion

        #region "POST REQUEST"
        [HttpPost]
        [Route("save")]
       // [ValidateModelState]
        public HttpResponseMessage SaveOTRequest([FromBody] Header objHeader, [FromUri]int? ReqID = null)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<OTRequest> transaction = new TranInfo<OTRequest>();
            try
            {
                ExtractClaimDetails();

                IList<string> Warnings;
                int reqid = service.SaveOTRequest(ReqID, objHeader, Username, out Warnings);
                //transaction.data = reqid;
                transaction.data = service.GetOTRequest(reqid, null, Username);
                transaction.warnings = Warnings;
                transaction.status = true;
            }
            catch (Exception ex)
            {
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;

                if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                LogHelper.Instance.Error(service.CorrelationId, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<OTRequest>>(ReturnCode, transaction);
        }
        [HttpPost]
        [Route("saveCust")]
        // [ValidateModelState]
        public HttpResponseMessage SaveCustDetails([FromBody] CustDetails objCustDetails, [FromUri]int? CustID = null)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<OTRequest> transaction = new TranInfo<OTRequest>();
            try
            {
                ExtractClaimDetails();

                IList<string> Warnings;
                int custid = service.SaveCustDetails(CustID, objCustDetails, Username, out Warnings);
                //transaction.data = reqid;
                //transaction.data = service.GetOTRequest(reqid, null, Username);
                transaction.warnings = Warnings;
                transaction.status = true;
            }
            catch (Exception ex)
            {
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;

                if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                LogHelper.Instance.Error(service.CorrelationId, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<OTRequest>>(ReturnCode, transaction);
        }

        [HttpPost]
        [Route("notes/save")]
        public HttpResponseMessage SaveNotes([FromUri]int ReqID, [FromBody]Notes objNotes)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();

            try
            {
                ExtractClaimDetails();

                transaction.data = service.SaveNotes(ReqID, objNotes, Username);
                transaction.status = true;
            }
            catch (Exception ex)
            {
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;

                if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                LogHelper.Instance.Error(service.CorrelationId, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }
        //all POST requests goes here
        #endregion
    }
}