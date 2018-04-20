using ApprovalWorkFlow.DataAccess;
using ApprovalWorkflowApp.UtilityClass;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ApprovalWorkflowApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<AWFContext>(null);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            try
            {
                Application.Lock();
                string logonUserName = string.Empty;
                string logonUserId = string.Empty;
                Application.UnLock();

                // get the user's Name since using Windows Authentication
                logonUserId = HttpContext.Current.User.Identity.Name.ToString();
                logonUserName = LDAPinfoCollect.GetUserName(logonUserId);
                Session["CurrentUser"] = logonUserName;
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}
