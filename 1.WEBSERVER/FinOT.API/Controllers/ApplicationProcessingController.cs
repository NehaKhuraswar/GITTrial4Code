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
    [RoutePrefix("api/applicationprocessing")]
    public class ApplicationProcessingController : ApiController
    {
      
        private int UserID;
        private readonly IApplicationProcessingService _service;
        private readonly ICommonService _commonService;
        private readonly IDocumentService _docService;
        private IExceptionHandler _eHandler;
       
       
        public ApplicationProcessingController()
        {
            _service = RAPDependancyResolver.Instance.GetKernel().Get<IApplicationProcessingService>();           
            _docService = RAPDependancyResolver.Instance.GetKernel().Get<IDocumentService>();
            _commonService = RAPDependancyResolver.Instance.GetKernel().Get<ICommonService>();
            _eHandler = RAPDependancyResolver.Instance.GetKernel().Get<IExceptionHandler>();
        }
        public ApplicationProcessingController(IApplicationProcessingService service)
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

        #region "GET REQUESTS"
      

        [AllowAnonymous]
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

               // if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //_commonService.LogError(errorCode, ex.Message, ex.InnerException.StackTrace.ToString(), 23, "GetRent");
                //  LogHelper.Instance.Error(service.CorrelationId, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<List<Rent>>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("getcaseinfo/{userid:int}")]
        [HttpPost]
        public HttpResponseMessage GetCaseDetails( int userid)
        {

            //Appl accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                //if (caseInfo != null)
                //{
                //    result = _service.GetCaseDetails(caseInfo.CaseID);
                //}
                //else
                //{
                    result = _service.GetCaseDetails(userid);
                //}
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

            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("getcaseinfo/{caseID}/{CustomerID:int}")]
        [HttpGet]
        public HttpResponseMessage GetCaseDetails(string caseID, int CustomerID)
        {

            //Appl accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                result = _service.GetCaseInfo(caseID,  CustomerID);
                if(result.status.Status == StatusEnum.Success)
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

            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("getappealgroundinfo/{CaseNumber}/{AppealFiledBy:int}")]
        [HttpGet]
        public HttpResponseMessage GetAppealGroundInfo(string CaseNumber, int AppealFiledBy)           
        {

            //Appl accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<AppealGroundM>> transaction = new TranInfo<List<AppealGroundM>>();
            ReturnResult<List<AppealGroundM>> result = new ReturnResult<List<AppealGroundM>>();
            try
            {

                result = _service.GetAppealGroundInfo(CaseNumber, AppealFiledBy);
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

            return Request.CreateResponse<TranInfo<List<AppealGroundM>>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("getappealserve/{AppealID:int}")]
        [HttpGet]
        public HttpResponseMessage GetAppealServe( int AppealID)
        {

            //Appl accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                result = _service.GetAppealServe(AppealID);
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

            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetUploadedDocuments/{CustomerID:int}/{DocumentTitle}")]
        [HttpGet]
        public HttpResponseMessage GetUploadedDocuments(int CustomerID, string DocumentTitle)
        {

            //Appl accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<DocumentM>> transaction = new TranInfo<List<DocumentM>>();
            ReturnResult<List<DocumentM>> result = new ReturnResult<List<DocumentM>>();
            try
            {

                result = _service.GetUploadedDocuments(CustomerID, DocumentTitle);
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
            return Request.CreateResponse<TranInfo<List<DocumentM>>>(ReturnCode, transaction);
        }
        #region Common File Petition method      
        [AllowAnonymous]
        [Route("GetPetitioncategory")]
        [HttpGet]
        public HttpResponseMessage GetPetitioncategory()
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _service.GetPetitioncategory();
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetPageSubmissionStatus/{CustomerID:int}")]
        [HttpGet]
        public HttpResponseMessage GetPageSubmissionStatus(int CustomerID)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<PetitionPageSubnmissionStatusM> transaction = new TranInfo<PetitionPageSubnmissionStatusM>();
            ReturnResult<PetitionPageSubnmissionStatusM> result = new ReturnResult<PetitionPageSubnmissionStatusM>();
            try
            {

                result = _service.GetPageSubmissionStatus(CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<PetitionPageSubnmissionStatusM>>(ReturnCode, transaction);
        }
      

        [AllowAnonymous]
        [Route("GetAppealPageSubmissionStatus/{CustomerID:int}")]
        [HttpGet]
        public HttpResponseMessage GetAppealPageSubmissionStatus(int CustomerID)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<AppealPageSubnmissionStatusM> transaction = new TranInfo<AppealPageSubnmissionStatusM>();
            ReturnResult<AppealPageSubnmissionStatusM> result = new ReturnResult<AppealPageSubnmissionStatusM>();
            try
            {

                result = _service.GetAppealPageSubmissionStatus(CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<AppealPageSubnmissionStatusM>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [Route("GetTRPageSubmissionStatus/{CustomerID:int}")]
        [HttpGet]
        public HttpResponseMessage GetTRPageSubmissionStatus(int CustomerID)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<TenantResponsePageSubnmissionStatusM> transaction = new TranInfo<TenantResponsePageSubnmissionStatusM>();
            ReturnResult<TenantResponsePageSubnmissionStatusM> result = new ReturnResult<TenantResponsePageSubnmissionStatusM>();
            try
            {

                result = _service.GetTRPageSubmissionStatus(CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<TenantResponsePageSubnmissionStatusM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetDocument")]
        [HttpPost]
        public HttpResponseMessage GetDocument([FromBody] DocumentM document)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<DocumentM> transaction = new TranInfo<DocumentM>();
            ReturnResult<DocumentM> result = new ReturnResult<DocumentM>();
            try
            {
                result = _docService.DownloadDocument(document);
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
            }
            return Request.CreateResponse<TranInfo<DocumentM>>(ReturnCode, transaction);
        }


        [AllowAnonymous]
        [Route("GetOResponseSubmissionStatus/{CustomerID:int}")]
        [HttpGet]
        public HttpResponseMessage GetOResponseSubmissionStatus(int CustomerID)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<OwnerResponsePageSubnmissionStatusM> transaction = new TranInfo<OwnerResponsePageSubnmissionStatusM>();
            ReturnResult<OwnerResponsePageSubnmissionStatusM> result = new ReturnResult<OwnerResponsePageSubnmissionStatusM>();
            try
            {

                result = _service.GetOResponseSubmissionStatus(CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<OwnerResponsePageSubnmissionStatusM>>(ReturnCode, transaction);
        }


        [AllowAnonymous]
        [Route("GetDocDescription")]
        [HttpGet]
        public HttpResponseMessage GetDocDescription()
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<string>> transaction = new TranInfo<List<string>>();
            ReturnResult<List<string>> result = new ReturnResult<List<string>>();
            try
            {
                result = _commonService.GetDocDescription();
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
            }
            return Request.CreateResponse<TranInfo<List<string>>>(ReturnCode, transaction);
        }

        #endregion   

        #endregion

        #region "POST REQUEST"

        [AllowAnonymous]
        [Route("submittenantpetition")]
        [HttpPost]
        public HttpResponseMessage SubmitTenantPetition([FromBody] CaseInfoM caseInfo)
        {
            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {


                result = _service.SubmitTenantPetition(caseInfo);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("submittenantresponse")]
        [HttpPost]
        public HttpResponseMessage SubmitTenantResponse([FromBody] CaseInfoM caseInfo)
        {
            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {


                result = _service.SubmitTenantResponse(caseInfo);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [Route("savecaseinfo")]
        [HttpPost]
        public HttpResponseMessage SaveCaseDetails([FromBody] CaseInfoM caseInfo)
        {
            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
              //var doc = getDoc();
              //var docServiceResult =   _docService.UploadDocument(doc);
                //if(docServiceResult.status.Status != StatusEnum.Success)
                //{
                //    transaction.status = false;
                //}
                result = _service.SaveCaseDetails(caseInfo);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("submitappeal")]
        [HttpPost]
        public HttpResponseMessage SubmitAppeal([FromBody] CaseInfoM caseInfo)
        {
            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                //var doc = getDoc();
                //var docServiceResult =   _docService.UploadDocument(doc);
                //if(docServiceResult.status.Status != StatusEnum.Success)
                //{
                //    transaction.status = false;
                //}
                result = _service.SubmitAppeal(caseInfo);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }
        
        [AllowAnonymous]
        [Route("getgroundsinfo/{petitionID:int}")]
        [HttpGet]
        public HttpResponseMessage GetPetitionGroundInfo(int petitionID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<PetitionGroundM>> transaction = new TranInfo<List<PetitionGroundM>>();
            ReturnResult<List<PetitionGroundM>> result = new ReturnResult<List<PetitionGroundM>>();
            try
            {

                result = _service.GetPetitionGroundInfo(petitionID);
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
            }
            return Request.CreateResponse<TranInfo<List<PetitionGroundM>>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("gettenantresponseexemptcontestedinfo/{TenantResponseID:int}")]
        [HttpGet]
        public HttpResponseMessage GetTenantResponseExemptContestedInfo(int TenantResponseID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                result = _service.GetTenantResponseExemptContestedInfo(TenantResponseID);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [Route("gettenantresponsereviewinfo/{CaseNumber}/{CustomerID:int}")]
        [HttpGet]
        public HttpResponseMessage GetTenantResponseReviewInfo(string CaseNumber, int CustomerID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                result = _service.GetTenantResponseReviewInfo( CaseNumber, CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("gettenantresponseapplicationinfo/{CaseNumber}/{CustomerID:int}")]
        [HttpGet]
        public HttpResponseMessage GetTenantResponseApplicationInfo(string CaseNumber, int CustomerID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                result = _service.GetTenantResponseApplicationInfo( CaseNumber, CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("getapplicationinfo/{CustomerID:int}")]
        [HttpGet]
        public HttpResponseMessage GetTenantApplicationInfo(int CustomerID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<TenantPetitionInfoM> transaction = new TranInfo<TenantPetitionInfoM>();
            ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            try
            {

                result = _service.GetTenantApplicationInfo(CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<TenantPetitionInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("getcasesnoanalyst/{UserID:int}")]
        [HttpGet]
        public HttpResponseMessage GetCasesNoAnalyst(int UserID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<CaseInfoM>> transaction = new TranInfo<List<CaseInfoM>>();
            ReturnResult<List<CaseInfoM>> result = new ReturnResult<List<CaseInfoM>>();
            try
            {

                result = _service.GetCasesNoAnalyst(UserID);
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
            }
            return Request.CreateResponse<TranInfo<List<CaseInfoM>>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [Route("GetSelectedCase/{C_ID:int}")]
        [HttpGet]
        public HttpResponseMessage GetSelectedCase(int C_ID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                result = _service.GetSelectedCase(C_ID);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("getcasesforcustomer/{CustomerID:int}")]
        [HttpGet]
        public HttpResponseMessage GetCasesForCustomer(int CustomerID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<CaseInfoM>> transaction = new TranInfo<List<CaseInfoM>>();
            ReturnResult<List<CaseInfoM>> result = new ReturnResult<List<CaseInfoM>>();
            try
            {

                result = _service.GetCasesForCustomer(CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<List<CaseInfoM>>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetThirdPartyCasesForCustomer/{CustomerID:int}")]
        [HttpGet]
        public HttpResponseMessage GetThirdPartyCasesForCustomer(int CustomerID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<ThirdPartyCaseInfo>> transaction = new TranInfo<List<ThirdPartyCaseInfo>>();
            ReturnResult<List<ThirdPartyCaseInfo>> result = new ReturnResult<List<ThirdPartyCaseInfo>>();
            try
            {

                result = _service.GetThirdPartyCasesForCustomer(CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<List<ThirdPartyCaseInfo>>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [Route("UpdateThirdPartyAccessPrivilege/{CustomerID:int}")]
        [HttpPost]
        public HttpResponseMessage UpdateThirdPartyAccessPrivilege([FromBody]List<ThirdPartyCaseInfo> ThirdPartyCaseInfo, [FromUri] int CustomerID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<ThirdPartyCaseInfo>> transaction = new TranInfo<List<ThirdPartyCaseInfo>>();
            ReturnResult<List<ThirdPartyCaseInfo>> result = new ReturnResult<List<ThirdPartyCaseInfo>>();
            try
            {

                result = _service.UpdateThirdPartyAccessPrivilege(ThirdPartyCaseInfo, CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<List<ThirdPartyCaseInfo>>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("getrentalhistoryinfo/{PetitionId:int}")]
        [HttpGet]
        public HttpResponseMessage GetRentalHistoryInfo(int PetitionId)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<TenantRentalHistoryM> transaction = new TranInfo<TenantRentalHistoryM>();
            ReturnResult<TenantRentalHistoryM> result = new ReturnResult<TenantRentalHistoryM>();
            try
            {

                result = _service.GetRentalHistoryInfo(PetitionId);
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
            }
            return Request.CreateResponse<TranInfo<TenantRentalHistoryM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("gettenantresponserentalhistoryinfo/{TenantResponseID:int}/{CustomerID:int}")]
        [HttpGet]
        public HttpResponseMessage GetTenantResponseRentalHistoryInfo(int TenantResponseID, int CustomerID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<TenantResponseRentalHistoryM> transaction = new TranInfo<TenantResponseRentalHistoryM>();
            ReturnResult<TenantResponseRentalHistoryM> result = new ReturnResult<TenantResponseRentalHistoryM>();
            try
            {

                result = _service.GetTenantResponseRentalHistoryInfo(TenantResponseID, CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<TenantResponseRentalHistoryM>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [Route("gettenantlostservice/{PetitionId:int}/{CustomerID:int}")]
        [HttpGet]
        public HttpResponseMessage GetTenantLostServiceInfo(int PetitionId, int CustomerID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<LostServicesPageM> transaction = new TranInfo<LostServicesPageM>();
            ReturnResult<LostServicesPageM> result = new ReturnResult<LostServicesPageM>();
            try
            {

                result = _service.GetTenantLostServiceInfo(PetitionId, CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<LostServicesPageM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("gettenantreview/{CustomerID:int}")]
        [HttpGet]
        public HttpResponseMessage GetTenantReviewInfo(int CustomerID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<TenantPetitionInfoM> transaction = new TranInfo<TenantPetitionInfoM>();
            ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            try
            {

                result = _service.GetTenantReviewInfo(CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<TenantPetitionInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetPetitionViewInfo/{CID:int}")]
        [HttpGet]
        public HttpResponseMessage GetPetitionViewInfo(int CID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                result = _service.GetPetitionViewInfo(CID);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [Route("GetTenantAppealInfoForView/{CID:int}")]
        [HttpGet]
        public HttpResponseMessage GetTenantAppealInfoForView(int CID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                result = _service.GetTenantAppealInfoForView(CID);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [Route("GetTenantResponseViewInfo/{CID:int}")]
        [HttpGet]
        public HttpResponseMessage GetTenantResponseViewInfo(int CID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                result = _service.GetTenantResponseViewInfo(CID);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("getemptytrrentalhistoryinfo")]
        [HttpGet]
        public HttpResponseMessage GetEmptyTenantResponseRentalIncrementInfo()
        {


            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<TenantResponseRentIncreaseInfoM> transaction = new TranInfo<TenantResponseRentIncreaseInfoM>();
            ReturnResult<TenantResponseRentIncreaseInfoM> result = new ReturnResult<TenantResponseRentIncreaseInfoM>();
            try
            {

                TenantResponseRentIncreaseInfoM obj = new TenantResponseRentIncreaseInfoM();
                transaction.data = obj;
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
            return Request.CreateResponse<TranInfo<TenantResponseRentIncreaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("getemptyrentalhistoryinfo")]
        [HttpGet]
        public HttpResponseMessage GetEmptyTenantRentalIncrementInfo()
        {
            

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<TenantRentIncreaseInfoM> transaction = new TranInfo<TenantRentIncreaseInfoM>();
            ReturnResult<TenantRentIncreaseInfoM> result = new ReturnResult<TenantRentIncreaseInfoM>();
            try
            {

                TenantRentIncreaseInfoM obj = new TenantRentIncreaseInfoM();
                transaction.data = obj;
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
            return Request.CreateResponse<TranInfo<TenantRentIncreaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("getopposingparty")]
        [HttpGet]
        public HttpResponseMessage GetOpposingParty()
        {


            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<UserInfoM> transaction = new TranInfo<UserInfoM>();
            ReturnResult<UserInfoM> result = new ReturnResult<UserInfoM>();
            try
            {

                UserInfoM obj = new UserInfoM();
                transaction.data = obj;
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
            return Request.CreateResponse<TranInfo<UserInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("getemptylostservicesinfo")]
        [HttpGet]
        public HttpResponseMessage GetEmptyLostServicesInfo()
        {


            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<TenantLostServiceInfoM> transaction = new TranInfo<TenantLostServiceInfoM>();
            ReturnResult<TenantLostServiceInfoM> result = new ReturnResult<TenantLostServiceInfoM>();
            try
            {

                TenantLostServiceInfoM obj = new TenantLostServiceInfoM();
                transaction.data = obj;
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
            return Request.CreateResponse<TranInfo<TenantLostServiceInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("getemptyproblemsinfo")]
        [HttpGet]
        public HttpResponseMessage GetEmptyProblemsInfo()
        {


            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<TenantProblemInfoM> transaction = new TranInfo<TenantProblemInfoM>();
            ReturnResult<TenantProblemInfoM> result = new ReturnResult<TenantProblemInfoM>();
            try
            {

                TenantProblemInfoM obj = new TenantProblemInfoM();
                transaction.data = obj;
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
            return Request.CreateResponse<TranInfo<TenantProblemInfoM>>(ReturnCode, transaction);
        }


        [AllowAnonymous]
        [Route("saveapplicationinfo")]
        [HttpPost]
        public HttpResponseMessage SaveApplicationInfo([FromBody] CaseInfoM caseInfo)
        {
            ExtractClaimDetails();
            
            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                result = _service.SaveApplicationInfo(caseInfo, UserID);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }  
        [AllowAnonymous]
        [Route("savetenantresponseexemptcontested/{CustomerID:int}")]
        [HttpPost]
        public HttpResponseMessage SaveTenantResponseExemptContestedInfo([FromBody] TenantResponseExemptContestedInfoM message, [FromUri]int CustomerID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                result = _service.SaveTenantResponseExemptContestedInfo(message, CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [Route("savetenantresponseapplicationinfo")]
        [HttpPost]
        public HttpResponseMessage SaveTenantResponseApplicationInfo([FromBody] CaseInfoM caseInfo)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                result = _service.SaveTenantResponseApplicationInfo(caseInfo, UserID);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SaveTenantDocuments/{CustomerID:int}")]
        [HttpPost]
        public HttpResponseMessage SaveTenantDocuments([FromBody] List<DocumentM> documents, [FromUri]int CustomerID)
        {
            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<DocumentM>> transaction = new TranInfo<List<DocumentM>>();
            ReturnResult<List<DocumentM>> result = new ReturnResult<List<DocumentM>>();
            try
            {

                result = _service.SaveTenantDocuments(documents, CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<List<DocumentM>>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [Route("savetenantlostserviceinfo/{CustomerID:int}")]
        [HttpPost]
        public HttpResponseMessage SaveTenantLostServiceInfo([FromBody] LostServicesPageM message, [FromUri] int CustomerID)
        {
            //Document upload sample - TBD
            //if(petition.File != null)
            //{
            //    ReturnResult<DocumentM> docUploadResult = _docService.UploadDocument(petition.File);
            //}
            ExtractClaimDetails();
           

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                result = _service.SaveTenantLostServiceInfo(message, CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("savepetitiongroundinfo/{CustomerID:int}")]
        [HttpPost]
        public HttpResponseMessage SavePetitionGroundInfo([FromBody] TenantPetitionInfoM petition, [FromUri] int CustomerID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                result = _service.SavePetitionGroundInfo(petition, CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("saverentalhistoryinfo/{CustomerID:int}")]
        [HttpPost]
        public HttpResponseMessage SaveTenantRentalHistoryInfo([FromBody] TenantRentalHistoryM rentalHistory, [FromUri]int CustomerID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                result = _service.SaveTenantRentalHistoryInfo(rentalHistory, CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("savetenantresponserentalhistoryinfo/{CustomerID:int}")]
        [HttpPost]
        public HttpResponseMessage SaveTenantResponseRentalHistoryInfo([FromBody] TenantResponseRentalHistoryM rentalHistory, [FromUri]int CustomerID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                result = _service.SaveTenantResponseRentalHistoryInfo(rentalHistory, CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }


        //TBD - to be removed as we dont need to save the APpeal info
        [AllowAnonymous]
        [Route("savetenantappealinfo/{CustomerID:int}")]
        [HttpPost]
        public HttpResponseMessage SaveTenantAppealInfo([FromBody] CaseInfoM caseInfo, [FromUri]int CustomerID)
        {
            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<TenantAppealInfoM> transaction = new TranInfo<TenantAppealInfoM>();
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            try
            {

                result = _service.SaveTenantAppealInfo(caseInfo, CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<TenantAppealInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("savetenantservingappeal/{CustomerID:int}")]
        [HttpPost]
        public HttpResponseMessage SaveTenantServingAppeal([FromBody] TenantAppealInfoM tenantAppealInfo, [FromUri] int CustomerID)
        {
            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<TenantAppealInfoM> transaction = new TranInfo<TenantAppealInfoM>();
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            try
            {

                result = _service.SaveTenantServingAppeal(tenantAppealInfo, CustomerID);
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
            }
            return Request.CreateResponse<TranInfo<TenantAppealInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("addopposingparty")]
        [HttpPost]
        public HttpResponseMessage AddAnotherOpposingParty([FromBody] TenantAppealInfoM tenantAppealInfo)
        {
            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                result = _service.AddAnotherOpposingParty(tenantAppealInfo);
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
            }
            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("saveappealgroundinfo")]
        [HttpPost]
        public HttpResponseMessage SaveAppealGroundInfo([FromBody] TenantAppealInfoM tenantAppealInfo)
        {
            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<TenantAppealInfoM> transaction = new TranInfo<TenantAppealInfoM>();
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            try
            {

                result = _service.SaveAppealGroundInfo(tenantAppealInfo);
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
            }
            return Request.CreateResponse<TranInfo<TenantAppealInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SaveAppeallDocuments")]
        [HttpPost]
        public HttpResponseMessage SaveAppeallDocuments([FromBody] List<DocumentM> documents)
        {
            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<DocumentM>> transaction = new TranInfo<List<DocumentM>>();
            ReturnResult<List<DocumentM>> result = new ReturnResult<List<DocumentM>>();
            try
            {

                result = _service.SaveAppeallDocuments(documents);
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
            }
            return Request.CreateResponse<TranInfo<List<DocumentM>>>(ReturnCode, transaction);
        }

        #region Owner Petition Methods
     
        [AllowAnonymous]
        [Route("GetOwnerApplicantInfo")]
        [HttpPost]
        public HttpResponseMessage GetOwnerApplicantInfo([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _service.GetOwnerApplicantInfo(model);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetRentIncreaseReasonInfo")]
        [HttpPost]
        public HttpResponseMessage GetRentIncreaseReasonInfo([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _service.GetRentIncreaseReasonInfo(model);
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
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetOwnerPropertyAndTenantInfo")]
        [HttpPost]
        public HttpResponseMessage GetOwnerPropertyAndTenantInfo([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _service.GetOwnerPropertyAndTenantInfo(model);
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
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }
       
        [AllowAnonymous]
        [Route("GetOwnerRentIncreaseAndPropertyInfo")]
        [HttpPost]
        public HttpResponseMessage GetOwnerRentIncreaseAndPropertyInfo([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _service.GetOwnerRentIncreaseAndPropertyInfo(model);
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
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetOwnerAdditionalDocuments")]
        [HttpPost]
        public HttpResponseMessage GetOwnerAdditionalDocuments([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _service.GetOwnerAdditionalDocuments(model);
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
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetOwnerReview")]
        [HttpPost]
        public HttpResponseMessage GetOwnerReview([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _service.GetOwnerReview(model);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetOwnerReviewByCaseID/{CID:int}")]
        [HttpGet]
        public HttpResponseMessage GetOwnerReviewByCaseID(int CID)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                result = _service.GetOwnerReviewByCaseID(CID);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetTRAdditionalDocuments")]
        [HttpPost]
        public HttpResponseMessage GetTRAdditionalDocuments([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _service.GetTRAdditionalDocuments(model);
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
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }
        
        [AllowAnonymous]
        [Route("SaveOwnerApplicantInfo")]
        [HttpPost]
        public HttpResponseMessage SaveOwnerApplicantInfo([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
               result = _service.SaveOwnerApplicantInfo(model);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SaveRentIncreaseReasonInfo")]
        [HttpPost]
        public HttpResponseMessage SaveRentIncreaseReasonInfo([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
               var dbResult = _service.SaveRentIncreaseReasonInfo(model);
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
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SaveOwnerPropertyAndTenantInfo")]
        [HttpPost]
        public HttpResponseMessage SaveOwnerPropertyAndTenantInfo([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _service.SaveOwnerPropertyAndTenantInfo(model);
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
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SaveOwnerRentIncreaseAndUpdatePropertyInfo")]
        [HttpPost]
        public HttpResponseMessage SaveOwnerRentIncreaseAndUpdatePropertyInfo([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _service.SaveOwnerRentIncreaseAndUpdatePropertyInfo(model);
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
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SaveOwnerAdditionalDocuments")]
        [HttpPost]
        public HttpResponseMessage SaveOwnerAdditionalDocuments([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _service.SaveOwnerAdditionalDocuments(model);
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
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SaveOwnerReviewPageSubmission/{CustomerID:int}")]
        [HttpPost]
        public HttpResponseMessage SaveOwnerReviewPageSubmission([FromUri]int CustomerID)
        {

            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                var dbResult = _service.SaveOwnerReviewPageSubmission(CustomerID);
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
            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SaveTRAdditionalDocuments")]
        [HttpPost]
        public HttpResponseMessage SaveTRAdditionalDocuments([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _service.SaveTRAdditionalDocuments(model);
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
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SubmitOwnerPetition")]
        [HttpPost]
        public HttpResponseMessage SubmitOwnerPetition([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _service.SubmitOwnerPetition(model);
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
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        #endregion

        #region Owner Response Methods
        [AllowAnonymous]
        [Route("GetOResponseApplicantInfo")]
        [HttpPost]
        public HttpResponseMessage GetOResponseApplicantInfo([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _service.GetOResponseApplicantInfo(model);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [Route("GetOResponsePropertyAndTenantInfo")]
        [HttpPost]
        public HttpResponseMessage GetOResponsePropertyAndTenantInfo([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _service.GetOResponsePropertyAndTenantInfo(model);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetOResponseRentIncreaseAndPropertyInfo")]
        [HttpPost]
        public HttpResponseMessage GetOResponseRentIncreaseAndPropertyInfo([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _service.GetOResponseRentIncreaseAndPropertyInfo(model);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetOResponseDecreasedHousing")]
        [HttpPost]
        public HttpResponseMessage GetOResponseDecreasedHousing([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _service.GetOResponseDecreasedHousing(model);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetOResponseExemption")]
        [HttpPost]
        public HttpResponseMessage GetOResponseExemption([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _service.GetOResponseExemption(model);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("GetOResponseReview")]
        [HttpPost]
        public HttpResponseMessage GetOResponseReview([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _service.GetOResponseReview(model);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }



        [AllowAnonymous]
        [Route("GetOResponseAdditionalDocuments")]
        [HttpPost]
        public HttpResponseMessage GetOResponseAdditionalDocuments([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _service.GetOResponseAdditionalDocuments(model);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SaveOResponseApplicantInfo")]
        [HttpPost]
        public HttpResponseMessage SaveOResponseApplicantInfo([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _service.SaveOResponseApplicantInfo(model);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SaveOResponsePropertyAndTenantInfo")]
        [HttpPost]
        public HttpResponseMessage SaveOResponsePropertyAndTenantInfo([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _service.SaveOResponsePropertyAndTenantInfo(model);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SaveOResponseRentIncreaseAndUpdatePropertyInfo")]
        [HttpPost]
        public HttpResponseMessage SaveOResponseRentIncreaseAndUpdatePropertyInfo([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _service.SaveOResponseRentIncreaseAndUpdatePropertyInfo(model);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SaveOResponseDecreasedHousing")]
        [HttpPost]
        public HttpResponseMessage SaveOResponseDecreasedHousing([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _service.SaveOResponseDecreasedHousing(model);
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
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SaveOResponseExemption")]
        [HttpPost]
        public HttpResponseMessage SaveOResponseExemption([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _service.SaveOResponseExemption(model);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SaveOResponseAdditionalDocuments")]
        [HttpPost]
        public HttpResponseMessage SaveOResponseAdditionalDocuments([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _service.SaveOResponseAdditionalDocuments(model);
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
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SaveOResponseReviewPageSubmission/{CustomerID:int}")]
        [HttpPost]
        public HttpResponseMessage SaveOResponseReviewPageSubmission([FromUri]int CustomerID)
        {

            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                var dbResult = _service.SaveOResponseReviewPageSubmission(CustomerID);
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
            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("SubmitOwnerResponse")]
        [HttpPost]
        public HttpResponseMessage SubmitOwnerResponse([FromBody] CaseInfoM model)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _service.SubmitOwnerResponse(model);
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
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }
        #endregion

        //[Route("addopposingparty")]
        //[HttpPost]
        //public HttpResponseMessage AddAnotherOpposingParty([FromBody] UserInfoM userInfo)
        //{
        //    //AccountManagementService accService = new AccountManagementService();
        //    HttpStatusCode ReturnCode = HttpStatusCode.OK;
        //    TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
        //    ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
        //    try
        //    {
        //        result = _service.SaveCaseDetails(caseInfo);
        //        if (result.status.Status == StatusEnum.Success)
        //        {
        //            transaction.data = result.result;
        //            transaction.status = true;
        //        }
        //        else
        //        {
        //            transaction.status = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.status = false;
        //        transaction.AddException(ex.Message);
        //        ReturnCode = HttpStatusCode.InternalServerError;
        //    }
        //    return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        //}
        //all POST requests goes here
        #endregion


        //TBR
        private DocumentM getDoc()
        {
            string filename = @"C:\Oakland\Ref Documents\flowMap.pdf";
            DocumentM doc = new DocumentM();
            doc.DocName = "RAPSecondDoc";
            doc.DocDescription = "RAPSecondDoc";
            //doc.DocType = "PDF";

            byte[] bArray = null;
            FileStream fs = new FileStream(filename,
                                           FileMode.Open,
                                           FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(filename).Length;
            bArray = br.ReadBytes((int)numBytes);
            doc.Content = bArray;
            return doc;
        }
    }
}
