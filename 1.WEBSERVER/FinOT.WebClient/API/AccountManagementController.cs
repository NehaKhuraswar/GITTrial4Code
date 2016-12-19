using System;
using System.Collections.Generic;
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
        private readonly string baseURL;
        private string requestURI = "api/accountmanagement/";
       
        public AccountManagementController()
        {
            baseURL = ConfigurationManager.AppSettings["APIBASEURL"].ToString();

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
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //string RequstURI = string.Format(ConfigurationManager.AppSettings[message.URI].ToString(), message.EventId);
                string requestUri = requestURI + "getaccounttypes/";

               responseMessage = client.GetAsync(requestUri).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage;
                }
                else // error
                {
                    responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
                return responseMessage;
            }
            catch
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return responseMessage;
            }          
        }

        [AllowAnonymous]
        [Route("logincust")]
        [HttpPost]
        //public HttpResponseMessage LoginCust([FromBody] CustomerInfo loginInfo) 
        public HttpResponseMessage LoginCust()
        {

            HttpResponseMessage responseMessage;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //string RequstURI = string.Format(ConfigurationManager.AppSettings[message.URI].ToString(), message.EventId);
                string requestUri = requestURI + "logincust34/";
                responseMessage = client.PostAsync(requestUri, Request.Content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage;
                }
                else // error
                {
                    responseMessage = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
                    responseMessage.ReasonPhrase = "Error occured in webclient API";
                }
                return responseMessage;
            }
            catch
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                responseMessage.ReasonPhrase = "Exception occured in webclient API";
                return responseMessage;
            }          
        }


    }
}