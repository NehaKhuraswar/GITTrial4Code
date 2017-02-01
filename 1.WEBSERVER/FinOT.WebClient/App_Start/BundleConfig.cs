using System.Web;
using System.Web.Optimization;

namespace RAP.WebClient
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"));
            
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                    "~/Scripts/angular.min.js"
                    , "~/Scripts/angular-route.js"
                    , "~/Scripts/angular-filter.min.js"
                    , "~/Scripts/angular-sanitize.min.js"
                    , "~/Scripts/angular-animate.min.js"
                    , "~/Scripts/angular-block-ui.min.js"
                    , "~/Scripts/angular-cookies.min.js"
                    , "~/Scripts/angular-inform.min.js" //,"~/Scripts/angular-showdown.js"
                    , "~/Scripts/ng-file-upload.min.js"
                    , "~/Scripts/select.js"
                    , "~/Scripts/lodash.min.js"
                    , "~/Scripts/angularjs-dropdown-multiselect.js"
                    , "~/Scripts/pagination/dirPagination.js"
                    , "~/Scripts/angular-ui/ui-bootstrap.js"
                    , "~/Scripts/angular-ui/ui-bootstrap-tpls.js"
                    , "~/js/*.js"
            ));

            //bundles.Add(new ScriptBundle("~/bundles/angularRAP").Include(
            //        "~/js/*.js"));
            

            bundles.Add(new PartialsBundle("RMS", "~/bundles/angularTemplates")
                .IncludeDirectory("~/Templates", "*.tpl.html", true));

            bundles.Add(new ScriptBundle("~/bundles/angularApp")
                .IncludeDirectory("~/Application/6.Config", "*.js", true)
                .IncludeDirectory("~/Application/5.Global", "*.js", true)
                .IncludeDirectory("~/Application/4.Services", "*.js", true)
                .IncludeDirectory("~/Application/3.Factories", "*.js", true)
                .IncludeDirectory("~/Application/2.Controllers", "*.js", true)
                .IncludeDirectory("~/Application/1.Modules", "*.js", true)
                .Include("~/Application/Application.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/angular-block-ui.min.css"
                    , "~/Content/angular-inform.min.css"));
                    //,"~/Content/bootstrap.css"
                    //, "~/Content/font-awesome.css"
                    //, "~/Content/select2.css"
                    //, "~/Content/select.css"
                    //,"~/Content/site.css"));

            //enable minification if compilation is otherthan DEBUG 
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
