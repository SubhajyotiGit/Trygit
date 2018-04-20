using System.Web;
using System.Web.Optimization;

namespace ApprovalWorkflowApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/AngularScripts").Include(
                "~/Scripts/angular.js",
                "~/Scripts/dirPagination.js",
                "~/Scripts/App/StatusDirective.js",
                 "~/Scripts/spin.js",
                "~/Scripts/angular-spinner.js",
                "~/Scripts/App/SubmitRequest.js",
                "~/Scripts/App/MyRequest.js",
                "~/Scripts/App/Audit.js",
                "~/Scripts/App/MyApproval.js",
                "~/Scripts/App/SupportApproval.js",
                "~/Scripts/App/RequestStatus.js"
               )); // "~/Scripts/angular-animate.js","~/Content/bootstrap-theme.css",   "~/Scripts/angular-toastr.tpls.js",, "~/Scripts/toaster.js",  "~/Content/toaster.css","~/Scripts/angular.js","~/Scripts/toaster.js",

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/angular-toastr.css",
                      "~/Content/site.css"));
        }
    }
}
