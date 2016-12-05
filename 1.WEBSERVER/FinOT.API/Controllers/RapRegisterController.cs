using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RAP.API.Common;
using System.Text;
using RAP.Core.FinServices.APIService;
using RAP.API.Models;
using System.Web.Http.Controllers;
using System.Security.Claims;
using RAP.Core.DataModels;
using RAP.Business.Implementation;
using RAP.Core.Services;
using Ninject;
using System.Net.Mail;
using RAP.Core.Common;
using RAP.Core.Services;
using RAP.Business.Binding;

namespace RAP.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/accountmanagement")]
    public class RapRegisterController : ApiController
    {
        string Username, CorrelationID, ExceptionMessage, InnerExceptionMessage;
        private readonly ICommonService _commonService;
        private readonly IExceptionHandler _eHandler;
        private readonly string errorCode = "5555";       
      
        public RapRegisterController()
        {
            _commonService = RAPDependancyResolver.Instance.GetKernel().Get<ICommonService>();
            _eHandler = RAPDependancyResolver.Instance.GetKernel().Get<IExceptionHandler>();
        }

        public void ExtractClaimDetails()
        {
            HttpRequestContext context = Request.GetRequestContext();
            var principle = Request.GetRequestContext().Principal as ClaimsPrincipal;
            CorrelationID = principle.Claims.Where(x => x.Type == ClaimTypes.SerialNumber).FirstOrDefault().Value;
            Username = principle.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
            ExceptionMessage = "An error occured while processing your request. Reference# " + CorrelationID;
        }

        [AllowAnonymous]
        [Route("header/{username}")]
        public ApplicationHeader GetAppHeader(string username)
        {
            try
            {
                APIHelper api = new APIHelper();
                ApplicationHeader header = null;// api.GetAppHeader(username, string.Empty);

                ////limit menus to the list that user has access to.
                //IList<int> userroles = header.UserPrivileges.Select(s => s.RoleId).Distinct().ToList<int>();
                //IList<string> pagetags = header.PageRoles.Where(w => userroles.Contains(w.RoleId)).Select(s => s.PageTag).ToList<string>();
                //header.Menus = header.Menus.Where(w => pagetags.Contains(w.PageTag)).Select(s => s).ToArray();

                return header;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("get/{custid:int?}")]
        public HttpResponseMessage GetCustomer(int? custid = null)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CustomerInfo> transaction = new TranInfo<CustomerInfo>();
            ReturnResult<CustomerInfo> result = new ReturnResult<CustomerInfo>();

            try
            {
              //  ExtractClaimDetails();

                CustomerInfo obj;
                //if (custid.HasValue)
                
                //{
                //  //  obj = service.GetCustomer((int)reqid, fy, Username);
                //}
                //else
                //{
                obj = new CustomerInfo();
                //}

                transaction.data = obj;
                transaction.status = true;
            }
            catch (Exception ex)
            {
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);

                //  LogHelper.Instance.Error(service.CorrelationId, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<CustomerInfo>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("logincust")]
        [HttpPost]
        public HttpResponseMessage LoginCust([FromBody] CustomerInfo loginInfo)
        
        {
            System.Diagnostics.EventLog.WriteEntry("Application", "LoginCust started");
            AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CustomerInfo> transaction = new TranInfo<CustomerInfo>();
            ReturnResult<CustomerInfo> result = new ReturnResult<CustomerInfo>();

            try
            {
                result = accService.GetCustomer(loginInfo);
                if (result.status.Status == StatusEnum.Success)
                {
                    transaction.data = result.result;
                    transaction.status = true;
                }
                else
                {
                   // transaction.warnings.Add(result.status.StatusMessage);
                    
                    transaction.status = false;
                    transaction.AddException(result.status.StatusMessage);

                    //_commonService.LogError(result.status.StatusCode, result.status.StatusMessage, result.status.StatusDetails, 0, "LoginCust");
                }
                

            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
               // transaction.AddException(ex.Message);
                //ReturnCode = HttpStatusCode.InternalServerError;

                //if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //LogHelper.Instance.Error(CorrelationID, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<CustomerInfo>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("getaccounttypes")]
        public HttpResponseMessage GetAccountTypes()
        {
            AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<AccountType>> transaction = new TranInfo<List<AccountType>>();
            ReturnResult<List<AccountType>> result = new ReturnResult<List<AccountType>>();
            try
            {
                result = accService.GetAccountTypes();

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

            return Request.CreateResponse<TranInfo<List<AccountType>>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getemptyaccountsearchmodel")]
        public HttpResponseMessage GetEmptyAccountSearchModel()
        {
           
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<AccountSearch> transaction = new TranInfo<AccountSearch>();
            ReturnResult<AccountSearch> result = new ReturnResult<AccountSearch>();
            
            try
            {
                

                transaction.data =  new AccountSearch();
                transaction.status = true;
                
            }
            catch (Exception ex)
            {
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
            }

            return Request.CreateResponse <TranInfo<AccountSearch>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("authorizedusers/{custid:int?}")]
        [HttpGet]
        public HttpResponseMessage GetAuthorizedUsers(int? custID = null)
        {
            AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<List<ThirdPartyDetails>> transaction = new TranInfo<List<ThirdPartyDetails>>();
            ReturnResult<List<ThirdPartyDetails>> result = new ReturnResult<List<ThirdPartyDetails>>();
            try
            {


                result = accService.GetAuthorizedUsers((int)custID);
                if (result.status.Status == StatusEnum.Success)
                {
                    transaction.data = result.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                    transaction.AddException(result.status.StatusMessage);
                    //_commonService.LogError(result.status.StatusCode, result.status.StatusMessage, result.status.StatusDetails, 23, "GetAuthorizedUsers");
                }


            }
            catch (Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                //ReturnCode = HttpStatusCode.InternalServerError;

                //if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //LogHelper.Instance.Error(CorrelationID, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<List<ThirdPartyDetails>>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [Route("searchinvite")]
        [HttpPost]
        public HttpResponseMessage SearchInviteThirdPartyUser([FromBody] CustomerInfo loginInfo)
        {
            AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CustomerInfo> transaction = new TranInfo<CustomerInfo>();
            ReturnResult<CustomerInfo> result = new ReturnResult<CustomerInfo>();
            try
            {

                
                result = accService.SearchInviteThirdPartyUser(loginInfo.email);
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
                // transaction.AddException(ex.Message);
                //ReturnCode = HttpStatusCode.InternalServerError;

                //if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //LogHelper.Instance.Error(CorrelationID, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<CustomerInfo>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("authorize/{custid:int?}")]
        [HttpPost]
        public HttpResponseMessage AuthorizeThirdPartyUser([FromBody] CustomerInfo thirdpartyInfo, int? custid = null)
        {
            AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CustomerInfo> transaction = new TranInfo<CustomerInfo>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                result = accService.AuthorizeThirdPartyUser((int)custid, thirdpartyInfo.custID);
                if (result.status.Status == StatusEnum.Success)
                {
                    //transaction.data = (bool)result.result;
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
                // transaction.AddException(ex.Message);
                //ReturnCode = HttpStatusCode.InternalServerError;

                //if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //LogHelper.Instance.Error(CorrelationID, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<CustomerInfo>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("removethirdparty/{custid:int?}")]
        [HttpPost]
        public HttpResponseMessage RemoveThirdParty([FromBody] ThirdPartyDetails thirdpartyInfo, int? custid = null)
        {
            AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                
                result = accService.RemoveThirdParty((int)custid, thirdpartyInfo.ThirdPartyRepresentationID);
                if (result.status.Status == StatusEnum.Success)
                {
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

                // transaction.AddException(ex.Message);
                //ReturnCode = HttpStatusCode.InternalServerError;

                //if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //LogHelper.Instance.Error(CorrelationID, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("getaccountsearch")]
        [HttpPost]
        public HttpResponseMessage GetAccountSearch([FromBody] AccountSearch accountSearch)
        {
            System.Diagnostics.EventLog.WriteEntry("Account", "Controller Get account search started");
            AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<SearchResult> transaction = new TranInfo<SearchResult>();
            ReturnResult<SearchResult> result = new ReturnResult<SearchResult>();

            try
            {
                result = accService.GetAccountSearch(accountSearch);
                if (result.status.Status == StatusEnum.Success)
                {
                    transaction.data = result.result;
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                    transaction.AddException(result.status.StatusMessage + result.status.StatusDetails);

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
            return Request.CreateResponse<TranInfo<SearchResult>>(ReturnCode, transaction);
        }

        [AllowAnonymous]
        [Route("saveCust")]
        [HttpPost]
        public HttpResponseMessage SaveCustomer([FromBody] CustomerInfo custModel)
        {
            System.Diagnostics.EventLog.WriteEntry("Application", "Controller Save Customer started");
            AccountManagementService accService = new AccountManagementService();
            IEmailService emailService = new EmailService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CustomerInfo> transaction = new TranInfo<CustomerInfo>();
            ReturnResult<CustomerInfo> result = new ReturnResult<CustomerInfo>();

            try
            {
                result = accService.SaveCustomer(custModel);
                if (result.status.Status == StatusEnum.Success)
                {
                    emailService.SendEmail(getRegisterCustomerEmailModel(result.result));
                    transaction.status = true;
                }
                else
                {
                    transaction.status = false;
                    transaction.AddException(result.status.StatusMessage + result.status.StatusDetails);                    
                    
                }
            }
            catch(Exception ex)
            {

                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
            }
            return Request.CreateResponse<TranInfo<CustomerInfo>>(ReturnCode, transaction);
        }
        [AllowAnonymous]
        [Route("invite")]
        [HttpPost]
        public HttpResponseMessage Invite([FromBody] CustomerInfo custModel)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<bool> transaction = new TranInfo<bool>();
  
            MailMessage mail = new MailMessage("nehab.infy@gmail.com", "neha.bhandari@gcomsoft.com");
            
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("", "");
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            mail.Subject = "this is a test email.";
            mail.Body = "this is my test email body";
            try
            {
                client.Send(mail);
            }
            catch(Exception ex)
            {
                transaction.status = false;
            }
            transaction.data = true;

            return Request.CreateResponse<TranInfo<bool>>(ReturnCode, transaction);
        }

        //TBD to be removed
        private EmailM getRegisterCustomerEmailModel(CustomerInfo customer)
        {
            EmailM model = new EmailM();
            model.Subject = "RAP Account registered successfully. CustomerIDentityKey :" + customer.CustomerIdentityKey.ToString();
            string[] toAddresses = { "venky.soundar@gcomsoft.com", "neha.bhandari@gcomsoft.com", "sanjay@gcomsoft.com" };
            model.RecipientAddress = toAddresses;
            model.MessageBody = "Hello" + customer.User.FirstName + " " + customer.User.LastName + ",  Your account created successfully";
            return model;
        }

    }
}
