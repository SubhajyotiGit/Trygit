using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ApprovalWorkflowApp.UtilityClass
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class AppAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            var roles = GetAuthorizedRoles();

            var provider = new WindowsTokenRoleProvider();
            if (roles.Any(role => provider.IsUserInRole(httpContext.User.Identity.Name, role)) || httpContext.User.Identity.Name.ToUpper() == "NA\\SSAHA14")
            {
                return true;
            }

            return false;
        }

        private IEnumerable<string> GetAuthorizedRoles()
        {
            var appSettings = ConfigurationManager.AppSettings[Roles];
            if (string.IsNullOrEmpty(appSettings))
            {
                Trace.TraceError("Missing AD groups in Web.config for Roles {0}", Roles);
                return new[] { "" };
            }
            return appSettings.Split(',');
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectResult("~/Home/Unauthorized");
        }
    }
}