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
using RAP.Core.Common;
using RAP.API.Models;
using RAP.API.Common;
//TBD
using RAP.Business.Implementation;
using System.IO;

namespace RAP.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/applicationprocessing")]
    public class ApplicationProcessingController : ApiController
    {
        private string Username, ExceptionMessage, InnerExceptionMessage;
        private int UserID;
        private readonly IApplicationProcessingService _service;
        private readonly IdocumentService _docService;
       
        public ApplicationProcessingController()
        {
            _service = new ApplicationProcessingService();
            _docService = new DocumentService();
        }
        public ApplicationProcessingController(IApplicationProcessingService service)
        {
            _service = service;
        }

        public void ExtractClaimDetails()
        {
            UserID = 20;
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

        [AllowAnonymous]
        [Route("getcaseinfo")]
        [HttpPost]
        public HttpResponseMessage GetCaseDetails( CaseInfoM caseInfo )
        {

            //Appl accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                result = _service.GetCaseDetails(caseInfo.CaseID);
                if (result.status.Status == StatusEnum.Success)
                {
                    transaction.data = result.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                }

            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;

                //if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //LogHelper.Instance.Error(CorrelationID, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("getcaseinfo")]
        [HttpGet]
        public HttpResponseMessage GetCaseDetails()
        {

            //Appl accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CaseInfoM> transaction = new TranInfo<CaseInfoM>();
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {

                result = _service.GetCaseDetails();
                if(result.status.Status == StatusEnum.Success)
                {
                    transaction.data = result.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                }

            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;

                //if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //LogHelper.Instance.Error(CorrelationID, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }
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
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
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
              var doc = getDoc();
              var docServiceResult =   _docService.UploadDocument(doc);
                if(docServiceResult.status.Status != StatusEnum.Success)
                {
                    transaction.status = false;
                }
                result = _service.SaveCaseDetails(caseInfo);
                if (result.status.Status == StatusEnum.Success)
                {
                    transaction.data = result.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
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
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
            }
            return Request.CreateResponse<TranInfo<CaseInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("savetenantlostserviceinfo")]
        [HttpPost]
        public HttpResponseMessage SaveTenantLostServiceInfo([FromBody] TenantPetitionInfoM petition)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                result = _service.SaveTenantLostServiceInfo(petition);
                if (result.status.Status == StatusEnum.Success)
                {
                    transaction.data = result.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
            }
            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("savepetitiongroundinfo")]
        [HttpPost]
        public HttpResponseMessage SavePetitionGroundInfo([FromBody] TenantPetitionInfoM petition)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                result = _service.SavePetitionGroundInfo(petition);
                if (result.status.Status == StatusEnum.Success)
                {
                    transaction.data = result.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
            }
            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("saverentalincrementinfo")]
        [HttpPost]
        public HttpResponseMessage SaveTenantRentalIncrementInfo([FromBody] TenantPetitionInfoM petition)
        {
            ExtractClaimDetails();

            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                result = _service.SaveTenantRentalIncrementInfo(petition);
                if (result.status.Status == StatusEnum.Success)
                {
                    transaction.data = result.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
            }
            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("savetenantappealinfo")]
        [HttpPost]
        public HttpResponseMessage SaveTenantAppealInfo([FromBody] CaseInfoM caseInfo)
        {
            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<TenantAppealInfoM> transaction = new TranInfo<TenantAppealInfoM>();
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            try
            {

                result = _service.SaveTenantAppealInfo(caseInfo);
                if (result.status.Status == StatusEnum.Success)
                {
                    transaction.data = result.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
            }
            return Request.CreateResponse<TranInfo<TenantAppealInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("savetenantservingappeal")]
        [HttpPost]
        public HttpResponseMessage SaveTenantServingAppeal([FromBody] CaseInfoM caseInfo)
        {
            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<TenantAppealInfoM> transaction = new TranInfo<TenantAppealInfoM>();
            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            try
            {

                result = _service.SaveTenantServingAppeal(caseInfo);
                if (result.status.Status == StatusEnum.Success)
                {
                    transaction.data = result.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
            }
            return Request.CreateResponse<TranInfo<TenantAppealInfoM>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("addopposingparty")]
        [HttpPost]
        public HttpResponseMessage AddAnotherOpposingParty([FromBody] CaseInfoM caseInfo)
        {
            //AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                result = _service.AddAnotherOpposingParty(caseInfo);
                if (result.status.Status == StatusEnum.Success)
                {
                    transaction.data = result.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
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
            TranInfo<Boolean> transaction = new TranInfo<Boolean>();
            ReturnResult<Boolean> result = new ReturnResult<Boolean>();
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
                }
            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
            }
            return Request.CreateResponse<TranInfo<Boolean>>(ReturnCode, transaction);
        }

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
            doc.DocTitle = "RAPSecondDoc";
            doc.DocType = "PDF";

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
