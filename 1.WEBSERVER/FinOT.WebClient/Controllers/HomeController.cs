using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RAP.WebClient.Models;
using RAP.WebClient.Common;
using RAP.Core.FinServices.APIService;
using System.Web.Configuration;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;

namespace RAP.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                AppModel model = (AppModel)Session[Constants.SESSION_APPMODEL];
                if (model == null)
                {
                    model = new AppModel();
                    string username = "HEALTH\\sannapaneni"; // User.Identity.Name;
                    string domain = username.Contains(@"\") ? username.Substring(0, username.IndexOf(@"\")) : null;
                    username = username.Contains(@"\") ? username.Substring(username.LastIndexOf(@"\") + 1) : username;
                    username = username.ToLower();

                    System.Net.WebClient client = new System.Net.WebClient();
                    string url = WebConfigurationManager.AppSettings[Constants.RCAPIBASE_URL] + string.Format(Constants.APPHEADER_URL, username);
                  //  model.Header = JsonConvert.DeserializeObject<RAP.Core.FinServices.APIService.ApplicationHeader>(client.DownloadString(url));

                    Session.Add(Constants.SESSION_APPMODEL, model);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
