using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Security.Claims;
using Ninject;
using RAP.Core.Services;
using RAP.Core.DataModels;
using RAP.Core.Common;
using RAP.API.Models;
using RAP.API.Common;
using RAP.Business.Binding;
//TBD
using RAP.Business.Implementation;
using System.IO;

namespace RAP.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/dashboard")]
    public class DashboardController : ApiController
    {
      
        private int UserID;
        private readonly IDashboardService _service;
        private readonly ICommonService _commonService;
        private readonly IdocumentService _docService;
        private IExceptionHandler _eHandler;
       
       
        public DashboardController()
        {
            _service = RAPDependancyResolver.Instance.GetKernel().Get<IDashboardService>();           
            _docService = RAPDependancyResolver.Instance.GetKernel().Get<IdocumentService>();
            _commonService = RAPDependancyResolver.Instance.GetKernel().Get<ICommonService>();
            _eHandler = RAPDependancyResolver.Instance.GetKernel().Get<IExceptionHandler>();
        }
        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        public void ExtractClaimDetails()
        {
            UserID = 23;
            //HttpRequestContext context = Request.GetRequestContext();
            //var principle = Request.GetRequestContext().Principal as ClaimsPrincipal;
            //_service.CorrelationId = principle.Claims.Where(x => x.Type == ClaimTypes.SerialNumber).FirstOrDefault().Value;
            //Username = principle.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
            //UserID = Convert.ToInt32(principle.Claims.Where(x => x.Type == ClaimTypes.UserData).FirstOrDefault().Value);
            //ExceptionMessage = "An error occured while processing your request. Reference# " + _service.CorrelationId;
        }

        #region "GET REQUEST"

        [AllowAnonymous]
        [HttpGet]
        [Route("getstatus/{activityid:int?}")]
        public HttpResponseMessage GetStatus(int? activityid = null)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<Status_M>> transaction = new TranInfo<List<Status_M>>();
            ReturnResult<List<Status_M>> result = new ReturnResult<List<Status_M>>();
            try
            {
                result = _service.GetStatus((int)activityid);

                if (result.status.Status == StatusEnum.Success)
                {
                    transaction.data = result.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                    transaction.AddException(result.status.StatusMessage);
                }
            }
            catch (Exception ex)
            {
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);

                //  LogHelper.Instance.Error(service.CorrelationId, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<List<Status_M>>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("getactivity")]
        public HttpResponseMessage GetActivity()
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<Activity_M>> transaction = new TranInfo<List<Activity_M>>();
            ReturnResult<List<Activity_M>> result = new ReturnResult<List<Activity_M>>();
            try
            {
                result = _service.GetActivity();

                if (result.status.Status == StatusEnum.Success)
                {
                    transaction.data = result.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                    transaction.AddException(result.status.StatusMessage);
                }
            }
            catch (Exception ex)
            {
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
            }

            return Request.CreateResponse<TranInfo<List<Activity_M>>>(ReturnCode, transaction);
        }

        #endregion

        #region "POST REQUEST"

        [AllowAnonymous]
        [Route("getemptyactivitystatus")]
        [HttpPost]
        public HttpResponseMessage GetEmptyActivityStatus()
        {

            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<ActivityStatus_M> transaction = new TranInfo<ActivityStatus_M>();
            ReturnResult<ActivityStatus_M> result = new ReturnResult<ActivityStatus_M>();
            try
            {                
                ActivityStatus_M objActivityStatus = new ActivityStatus_M();
                transaction.data = objActivityStatus;
                transaction.status = true;                

            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
            }

            return Request.CreateResponse<TranInfo<ActivityStatus_M>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [Route("getcaseactivitystatus")]
        [HttpPost]
        public HttpResponseMessage GetCaseActivityStatus(CaseInfoM caseInfo)
        {

            //Appl accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<ActivityStatus_M>> transaction = new TranInfo<List<ActivityStatus_M>>();
            ReturnResult<List<ActivityStatus_M>> result = new ReturnResult<List<ActivityStatus_M>>();
            try
            {
                result = _service.GetActivityStatusForCase(caseInfo.C_ID);
                
                if (result.status.Status == StatusEnum.Success)
                {
                    transaction.data = result.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                    transaction.AddException(result.status.StatusMessage);                      
                }

            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);

                //if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //LogHelper.Instance.Error(CorrelationID, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<List<ActivityStatus_M>>>(ReturnCode, transaction);
        }
        #endregion
    }
}
