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
     [RoutePrefix("api/accountmanagement")]
    public class AccountManagementController : ApiController
    {
        private readonly string _baseURL;
        private string _requestURI = "api/accountmanagement/";
        public string _errorMessage = "Error occurred in webclient AccountManagementController";
        public string _exception = "Exception occurred in webclient AccountManagementController";
       
        public AccountManagementController()
        {
            _baseURL = ConfigurationManager.AppSettings["APIBASEURL"].ToString();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("get")]
        public HttpResponseMessage GetCustomer()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "get/";
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
        [Route("getaccounttypes")]
        public HttpResponseMessage GetAccountTypes()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "getaccounttypes/";
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
        [Route("logincust")]
        [HttpPost]
        public HttpResponseMessage LoginCust()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));               
                string requestUri = _requestURI + "logincust/";
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
        [Route("changepwd")]
        [HttpPost]
        public HttpResponseMessage ChangePassword()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "changepwd/";
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
        [Route("forgetpwd")]
        [HttpPost]
        public HttpResponseMessage ForgetPwd()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "forgetpwd/";
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
        [Route("resendpin")]
        [HttpPost]
        public HttpResponseMessage ResendPin()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "resendpin/";
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
        [Route("GetThirdPartyInfo/{CustomerID}")]
        [HttpGet]
        public HttpResponseMessage GetThirdPartyInfo(string CustomerID)
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "GetThirdPartyInfo/" + CustomerID;
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
        [Route("logincityuser")]
        [HttpPost]
        public HttpResponseMessage LoginCityUser()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "logincityuser/";
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
        [HttpGet]
        [Route("getstatelist")]
        public HttpResponseMessage GetStateList()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "getstatelist/";
                responseMessage = client.GetAsync(requestUri).Result;
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
        [HttpGet]
        [Route("getemptyaccountsearchmodel")]
        public HttpResponseMessage GetEmptyAccountSearchModel()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "getemptyaccountsearchmodel/";
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
        [Route("searchinvite")]
        [HttpPost]
        public HttpResponseMessage SearchInviteCollaborator()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "searchinvite/";
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
        [Route("authorizecollaborator")]
        [HttpPost]
        public HttpResponseMessage AuthorizeCollaborator()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "authorizecollaborator/";
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
        [Route("removethirdparty/{custid:int?}")]
        [HttpPost]
        public HttpResponseMessage RemoveThirdParty()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "removethirdparty/";
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
        [Route("getaccountsearch")]
        [HttpPost]
        public HttpResponseMessage GetAccountSearch()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "getaccountsearch/";
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
        [Route("saveCust")]
        [HttpPost]
        public HttpResponseMessage SaveCustomer()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "saveCust/";
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
        [Route("SaveOrUpdateThirdPartyInfo")]
        [HttpPost]
        public HttpResponseMessage SaveOrUpdateThirdPartyInfo()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "SaveOrUpdateThirdPartyInfo/";
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
        [Route("RemoveThirdPartyInfo")]
        [HttpPost]
        public HttpResponseMessage RemoveThirdPartyInfo()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "RemoveThirdPartyInfo/";
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
        [Route("editCust")]
        [HttpPost]
        public HttpResponseMessage EditCustomer()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "editCust/";
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
        [Route("createcityuseraccount")]
        [HttpPost]
        public HttpResponseMessage CreateCityUserAccount()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "createcityuseraccount/";
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
        [Route("invite")]
        [HttpPost]
        public HttpResponseMessage Invite()
        {
            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string requestUri = _requestURI + "invite/";
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

    }
}