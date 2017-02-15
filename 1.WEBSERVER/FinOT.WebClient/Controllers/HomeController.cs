using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RAP.WebClient.Models;
using RAP.WebClient.Common;
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
                    System.Net.WebClient client = new System.Net.WebClient();
                    string url = WebConfigurationManager.AppSettings[Constants.RAPAPIBASE_URL];             
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
