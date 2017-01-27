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
        private readonly IDocumentService _docService;
        private IExceptionHandler _eHandler;
       
       
        public DashboardController()
        {
            _service = RAPDependancyResolver.Instance.GetKernel().Get<IDashboardService>();           
            _docService = RAPDependancyResolver.Instance.GetKernel().Get<IDocumentService>();
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

        [AllowAnonymous]
        [HttpGet]
        [Route("gethearingofficers")]
        public HttpResponseMessage GetHearingOfficers()
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<CityUserAccount_M>> transaction = new TranInfo<List<CityUserAccount_M>>();
            ReturnResult<List<CityUserAccount_M>> result = new ReturnResult<List<CityUserAccount_M>>();
            try
            {
                result = _service.GetHearingOfficers();

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

            return Request.CreateResponse<TranInfo<List<CityUserAccount_M>>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getanalysts")]
        public HttpResponseMessage GetAnalysts()
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<CityUserAccount_M>> transaction = new TranInfo<List<CityUserAccount_M>>();
            ReturnResult<List<CityUserAccount_M>> result = new ReturnResult<List<CityUserAccount_M>>();
            try
            {
                result = _service.GetAnalysts();

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

            return Request.CreateResponse<TranInfo<List<CityUserAccount_M>>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("assignanalyst/{cID:int}/{AnalystUserID:int}")]
        public HttpResponseMessage AssignAnalyst(int cID, int AnalystUserID)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _service.AssignAnalyst(cID, AnalystUserID);

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

            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("assignhearingofficer/{cID:int}/{HearingOfficerUserID:int}")]
        public HttpResponseMessage AssignHearingOfficer(int cID, int HearingOfficerUserID)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _service.AssignHearingOfficer(cID, HearingOfficerUserID);

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

            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetCaseDocuments/{cid:int}")]
        [HttpGet]
        public HttpResponseMessage GetCaseDocuments(int cid)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<DocumentM>> transaction = new TranInfo<List<DocumentM>>();
            ReturnResult<List<DocumentM>> result = new ReturnResult<List<DocumentM>>();
            try
            {
                var dbResult = _service.GetCaseDocuments(cid);
                if (dbResult.status.Status == StatusEnum.Success)
                {
                    transaction.data = dbResult.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                    transaction.AddException(dbResult.status.StatusMessage);
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
            }
            return Request.CreateResponse<TranInfo<List<DocumentM>>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("GetEmptyCaseSearchModel")]
        public HttpResponseMessage GetEmptyCaseSearchModel()
        {

            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseSearch> transaction = new TranInfo<CaseSearch>();
            ReturnResult<CaseSearch> result = new ReturnResult<CaseSearch>();

            try
            {


                transaction.data = new CaseSearch();
                transaction.status = true;

            }
            catch (Exception ex)
            {
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
            }

            return Request.CreateResponse<TranInfo<CaseSearch>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [Route("GetCustomEmail/{cid:int}")]
        [HttpGet]
        public HttpResponseMessage GetCustomEmail(int cid)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CustomEmailM> transaction = new TranInfo<CustomEmailM>();
            ReturnResult<CustomEmailM> result = new ReturnResult<CustomEmailM>();
            try
            {
                var dbResult = _service.GetCustomEmail(cid);
                if (dbResult.status.Status == StatusEnum.Success)
                {
                    transaction.data = dbResult.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                    transaction.AddException(dbResult.status.StatusMessage);
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
            }
            return Request.CreateResponse<TranInfo<CustomEmailM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetMail")]
        [HttpGet]
        public HttpResponseMessage GetMail()
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<MailM> transaction = new TranInfo<MailM>();
            ReturnResult<MailM> result = new ReturnResult<MailM>();
            try
            {
                var dbResult = _service.GetMail();
                if (dbResult.status.Status == StatusEnum.Success)
                {
                    transaction.data = dbResult.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                    transaction.AddException(dbResult.status.StatusMessage);
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
            }
            return Request.CreateResponse<TranInfo<MailM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetCustomEmailNotification/{cID:int}/{AnalystUserID:int}")]
        public HttpResponseMessage GetCustomEmailNotification(int cID, int AnalystUserID)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CustomEmailM> transaction = new TranInfo<CustomEmailM>();
            ReturnResult<CustomEmailM> result = new ReturnResult<CustomEmailM>();
            try
            {
                result = _service.GetCustomEmailNotification(cID, AnalystUserID);

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

            return Request.CreateResponse<TranInfo<CustomEmailM>>(ReturnCode, transaction);
        }

        #endregion

        #region "POST REQUEST"

        [AllowAnonymous]
        [Route("getemptyactivitystatus")]
        [HttpGet]
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



        [AllowAnonymous]
        [Route("GetCaseSearch")]
        [HttpPost]
        public HttpResponseMessage GetCaseSearch(CaseSearch caseSearch)
        {

            //Appl accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<SearchCaseResult> transaction = new TranInfo<SearchCaseResult>();
            ReturnResult<SearchCaseResult> result = new ReturnResult<SearchCaseResult>();
            try
            {
                result = _service.GetCaseSearch(caseSearch);

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

            return Request.CreateResponse<TranInfo<SearchCaseResult>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("UpdateAPNAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateAPNAddress(APNAddress apnAddress)
        {

            //Appl accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<APNAddress> transaction = new TranInfo<APNAddress>();
            ReturnResult<APNAddress> result = new ReturnResult<APNAddress>();
            try
            {
                result = _commonService.UpdateAPNAddress(apnAddress);

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

            return Request.CreateResponse <TranInfo<APNAddress>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("savenewactivitystatus/{cid:int}")]
        [HttpPost]
        public HttpResponseMessage SaveNewActivityStatus(ActivityStatus_M activityStatus, int CID)
        {
            ExtractClaimDetails();
            //Appl accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _service.SaveNewActivityStatus(activityStatus, CID);

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

            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }

       

        [AllowAnonymous]
        [Route("SaveCaseDocuments")]
        [HttpPost]
        public HttpResponseMessage SaveCaseDocuments([FromBody] List<DocumentM> documents)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<DocumentM>> transaction = new TranInfo<List<DocumentM>>();
            ReturnResult<List<DocumentM>> result = new ReturnResult<List<DocumentM>>();
            try
            {
                var dbResult = _service.SaveCaseDocuments(documents);
                if (dbResult.status.Status == StatusEnum.Success)
                {
                    transaction.data = dbResult.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                    transaction.AddException(dbResult.status.StatusMessage);
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
            }
            return Request.CreateResponse<TranInfo<List<DocumentM>>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SubmitCustomEmail")]
        [HttpPost]
        public HttpResponseMessage SubmitCustomEmail([FromBody] CustomEmailM documents)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CustomEmailM> transaction = new TranInfo<CustomEmailM>();
            ReturnResult<CustomEmailM> result = new ReturnResult<CustomEmailM>();
            try
            {
                var dbResult = _service.SubmitCustomEmail(documents);
                if (dbResult.status.Status == StatusEnum.Success)
                {
                    transaction.data = dbResult.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                    transaction.AddException(dbResult.status.StatusMessage);
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
            }
            return Request.CreateResponse<TranInfo<CustomEmailM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SubmitMail")]
        [HttpPost]
        public HttpResponseMessage SubmitMail([FromBody] MailM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<MailM> transaction = new TranInfo<MailM>();
            ReturnResult<MailM> result = new ReturnResult<MailM>();
            try
            {
                var dbResult = _service.SubmitMail(model);
                if (dbResult.status.Status == StatusEnum.Success)
                {
                    transaction.data = dbResult.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                    transaction.AddException(dbResult.status.StatusMessage);
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
            }
            return Request.CreateResponse<TranInfo<MailM>>(ReturnCode, transaction);
        }

        
        #endregion
    }
}
