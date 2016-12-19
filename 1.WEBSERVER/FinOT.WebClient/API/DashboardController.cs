using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Configuration;
using System.Net;
using System.Net.Http;


namespace RAP.WebClient
{
    [RoutePrefix("api/dashboard")]
    public class DashboardController : ApiController
    {
        private readonly string _baseURL;
        private string _requestURI = "api/dashboard/";
        public string _errorMessage = "Error occurred in webclient DashboardController";
        public string _exception = "Exception occurred in webclient DashboardController";

        public DashboardController()
        {
            _baseURL = ConfigurationManager.AppSettings["APIBASEURL"].ToString();
        }

        #region "GET REQUEST"

        [AllowAnonymous]
        [HttpGet]
        [Route("getstatus/{activityid:int?}")]
        public HttpResponseMessage GetStatus(int? activityid = null)
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string strContent = (activityid != null) ? activityid.ToString() : string.Empty;
                string requestUri = _requestURI + "getstatus/" + strContent;
                responseMessage = client.GetAsync(requestUri).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage;
                }
                else // error
                {
                    responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    responseMessage.ReasonPhrase = _errorMessage;
                }
                return responseMessage;
            }
            catch
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                responseMessage.ReasonPhrase = _exception;
                return responseMessage;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getactivity")]
        public HttpResponseMessage GetActivity()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "getactivity/";
                responseMessage = client.GetAsync(requestUri).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage;
                }
                else // error
                {
                    responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    responseMessage.ReasonPhrase = _errorMessage;
                }
                return responseMessage;
            }
            catch
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                responseMessage.ReasonPhrase = _exception;
                return responseMessage;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("gethearingofficers")]
        public HttpResponseMessage GetHearingOfficers()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "gethearingofficers/";
                responseMessage = client.GetAsync(requestUri).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage;
                }
                else // error
                {
                    responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    responseMessage.ReasonPhrase = _errorMessage;
                }
                return responseMessage;
            }
            catch
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                responseMessage.ReasonPhrase = _exception;
                return responseMessage;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getanalysts")]
        public HttpResponseMessage GetAnalysts()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "getanalysts/";
                responseMessage = client.GetAsync(requestUri).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage;
                }
                else // error
                {
                    responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    responseMessage.ReasonPhrase = _errorMessage;
                }
                return responseMessage;
            }
            catch
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                responseMessage.ReasonPhrase = _exception;
                return responseMessage;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("assignanalyst/{cID:int}/{AnalystUserID:int}")]
        public HttpResponseMessage AssignAnalyst(int cID, int AnalystUserID)
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "assignanalyst/" + cID.ToString() + "/" + AnalystUserID.ToString();
                responseMessage = client.GetAsync(requestUri).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage;
                }
                else // error
                {
                    responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    responseMessage.ReasonPhrase = _errorMessage;
                }
                return responseMessage;
            }
            catch
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                responseMessage.ReasonPhrase = _exception;
                return responseMessage;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("assignhearingofficer/{cID:int}/{HearingOfficerUserID:int}")]
        public HttpResponseMessage AssignHearingOfficer(int cID, int HearingOfficerUserID)
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "assignhearingofficer/" + cID.ToString() + "/" + HearingOfficerUserID.ToString();
                responseMessage = client.GetAsync(requestUri).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage;
                }
                else // error
                {
                    responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    responseMessage.ReasonPhrase = _errorMessage;
                }
                return responseMessage;
            }
            catch
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                responseMessage.ReasonPhrase = _exception;
                return responseMessage;
            }
        }

        [AllowAnonymous]
        [Route("getemptyactivitystatus")]
        [HttpGet]
        public HttpResponseMessage GetEmptyActivityStatus()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "getemptyactivitystatus/";
                responseMessage = client.GetAsync(requestUri).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage;
                }
                else // error
                {
                    responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    responseMessage.ReasonPhrase = _errorMessage;
                }
                return responseMessage;
            }
            catch
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                responseMessage.ReasonPhrase = _exception;
                return responseMessage;
            }
        }
        #endregion

        #region "POST REQUEST"
        [AllowAnonymous]
        [Route("getcaseactivitystatus")]
        [HttpPost]
        public HttpResponseMessage GetCaseActivityStatus()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "getcaseactivitystatus/";
                responseMessage = client.PostAsync(requestUri, Request.Content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage;
                }
                else
                {
                    responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    responseMessage.ReasonPhrase = _errorMessage;
                }
                return responseMessage;
            }
            catch
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                responseMessage.ReasonPhrase = _exception;
                return responseMessage;
            }
        }
        
        
        [AllowAnonymous]
        [Route("savenewactivitystatus/{cid:int}")]
        [HttpPost]
        public HttpResponseMessage SaveNewActivityStatus(int CID)
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "savenewactivitystatus/" + CID.ToString();
                responseMessage = client.PostAsync(requestUri, Request.Content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage;
                }
                else
                {
                    responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    responseMessage.ReasonPhrase = _errorMessage;
                }
                return responseMessage;
            }
            catch
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                responseMessage.ReasonPhrase = _exception;
                return responseMessage;
            }
        }

        #endregion
    }
}