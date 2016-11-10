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
using System.Net.Mail;

namespace RAP.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/accountmanagement")]
    public class RapRegisterController : ApiController
    {
        string Username, CorrelationID, ExceptionMessage, InnerExceptionMessage;
        public RapRegisterController() { }

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
                Rent obj1 = new Rent() { id = 1, name = "New Rent" };
                Rent obj2 = new Rent() { id = 2, name = "New Rent" };
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
        [HttpGet]
        [Route("get/{custid:int?}")]
        public HttpResponseMessage GetCustomer(int? custid = null)
        {
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CustomerInfo> transaction = new TranInfo<CustomerInfo>();

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

                if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //  LogHelper.Instance.Error(service.CorrelationId, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<CustomerInfo>>(ReturnCode, transaction);
        }
        [Route("logincust")]
        [HttpPost]
        public HttpResponseMessage LoginCust([FromBody] CustomerInfo loginInfo)
        
        {
            AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CustomerInfo> transaction = new TranInfo<CustomerInfo>();

            try
            {

                CustomerInfo obj;
                obj = accService.GetCustomer(loginInfo);
                if (obj != null)
                {
                    transaction.data = obj;
                    transaction.status = true;
                }
                else
                {
                    transaction.data = null;
                    transaction.status = false;
                }
                

            }
            catch (Exception ex)
            {
               // transaction.AddException(ex.Message);
                //ReturnCode = HttpStatusCode.InternalServerError;

                //if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //LogHelper.Instance.Error(CorrelationID, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<CustomerInfo>>(ReturnCode, transaction);
        }
        [Route("authorizedusers/{custid:int?}")]
        [HttpPost]
        public HttpResponseMessage GetAuthorizedUsers(int? custID = null)
        {
            AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<ThirdPartyDetails> transaction = new TranInfo<ThirdPartyDetails>();

            try
            {

                ThirdPartyDetails obj;
                obj = accService.GetAuthorizedUsers((int)custID);
                if (obj != null)
                {
                    transaction.data = obj;
                    transaction.status = true;
                }
                else
                {
                    transaction.data = null;
                    transaction.status = false;
                }


            }
            catch (Exception ex)
            {
                // transaction.AddException(ex.Message);
                //ReturnCode = HttpStatusCode.InternalServerError;

                //if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //LogHelper.Instance.Error(CorrelationID, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<ThirdPartyDetails>>(ReturnCode, transaction);
        }
        [Route("searchinvite")]
        [HttpPost]
        public HttpResponseMessage SearchInviteThirdPartyUser([FromBody] CustomerInfo loginInfo)
        {
            AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CustomerInfo> transaction = new TranInfo<CustomerInfo>();

            try
            {

                CustomerInfo obj;
                obj = accService.SearchInviteThirdPartyUser(loginInfo.email);
                if (obj != null)
                {
                    transaction.data = obj;
                    transaction.status = true;
                }
                else
                {
                    transaction.data = null;
                    transaction.status = false;
                }


            }
            catch (Exception ex)
            {
                // transaction.AddException(ex.Message);
                //ReturnCode = HttpStatusCode.InternalServerError;

                //if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //LogHelper.Instance.Error(CorrelationID, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<CustomerInfo>>(ReturnCode, transaction);
        }
        [Route("authorize/{custid:int?}")]
        [HttpPost]
        public HttpResponseMessage AuthorizeThirdPartyUser([FromBody] CustomerInfo thirdpartyInfo, int? custid = null)
        {
            AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CustomerInfo> transaction = new TranInfo<CustomerInfo>();

            try
            {

                bool bCompleted;
                bCompleted = accService.AuthorizeThirdPartyUser((int)custid, thirdpartyInfo.custID);
                transaction.status = bCompleted;
                 


            }
            catch (Exception ex)
            {
                // transaction.AddException(ex.Message);
                //ReturnCode = HttpStatusCode.InternalServerError;

                //if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                //LogHelper.Instance.Error(CorrelationID, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<CustomerInfo>>(ReturnCode, transaction);
        }
       
        [Route("reportsource")]
        [HttpPost]
        public HttpResponseMessage GetReportSource([FromBody] Dictionary<string, string> objParams, [FromUri]int ReportID)
        {
            
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<ReportPage> transaction = new TranInfo<ReportPage>();

            try
            {
                ExtractClaimDetails();

                StringBuilder reportSource = new StringBuilder();
                APIHelper api = new APIHelper();
                var header = api.GetAppHeader(Username, CorrelationID);
                var allReports = header.Reports;
                var allPages = header.Pages;

                ReportDetails report = allReports.Where(s => s.Id == ReportID).FirstOrDefault();
                if (report != null)
                {
                    //reportSource.Append(Url.Content("~/WebForms/Report.aspx"));
                    reportSource.Append(Url.Content("/RAP/WebForms/Report.aspx"));
                    reportSource.Append(string.Format("?ReportName={0}", report.Name));
                    if (!string.IsNullOrEmpty(report.Parameters))
                    {
                        string paramValue = "", param = report.Parameters;
                        string[] stdParams = param.Split(',').ToArray();
                        foreach (string key in stdParams)
                        {
                            if (objParams.ContainsKey(key))
                            {
                                paramValue += (string.IsNullOrEmpty(paramValue) ? "" : "&") + key + "=" + objParams[key].ToString();

                                if (objParams.ContainsKey("Show" + key))
                                    paramValue += "&Show" + key + "=" + objParams["Show" + key].ToString();
                                else
                                    paramValue += "&Show" + key + "=false";
                            }
                            else
                            {
                                param = param.Replace("," + key, "");
                                param = param.Replace(key + ",", "");
                                param = param.Replace(key, "");
                            }
                        }

                        if (!string.IsNullOrEmpty(paramValue)) { paramValue = "&" + paramValue; }
                        
                        reportSource.Append(string.Format("&Parameters={0}{1}", param, paramValue));
                    }

                    reportSource.Append(string.Format("&ReportServer={0}", report.ReportServer));
                    reportSource.Append(string.Format("&ReportPath={0}", report.ReportPath));

                    //get page title
                    string pageTitle = string.Empty;
                    var page = allPages.Where(w => w.Id == ReportID).FirstOrDefault();
                    if (page != null) { pageTitle = page.Name; }

                    transaction.data = new ReportPage() { PageTitle = pageTitle, Source = reportSource.ToString() }; ;
                    transaction.status = true;
                }
            }
            catch (Exception ex)
            {
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;

                if (ex.InnerException != null) { InnerExceptionMessage = ex.InnerException.Message; }
                LogHelper.Instance.Error(CorrelationID, Username, Request.GetRequestContext().VirtualPathRoot, ex.Message, InnerExceptionMessage, 0, ex);
            }

            return Request.CreateResponse<TranInfo<ReportPage>>(ReturnCode, transaction);
        }
        

        [Route("saveCust")]
        [HttpPost]
        public HttpResponseMessage SaveCustomer([FromBody] CustomerInfo custModel)
        {
            AccountManagementService accService = new AccountManagementService();
            HttpStatusCode ReturnCode = HttpStatusCode.OK;
            TranInfo<CustomerInfo> transaction = new TranInfo<CustomerInfo>();

            try
            {
                transaction.status = accService.SaveCustomer(custModel);
            }
            catch(Exception ex)
            {
                transaction.status = false;
                transaction.AddException(ex.Message);
                ReturnCode = HttpStatusCode.InternalServerError;
            }
            return Request.CreateResponse<TranInfo<CustomerInfo>>(ReturnCode, transaction);
        }

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

    }
}
