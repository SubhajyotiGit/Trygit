using ApprovalWorkFlow.Business;
using ApprovalWorkFlow.BusinessDTOs;
using ApprovalWorkflowApp.UtilityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ApprovalWorkflowApp.Controllers
{
   // [Authorize(Roles = @"JNJ\JRD-APP-BCPASSAYMGMT-Approvers")]  //  [Authorize(Roles = @"JNJ\JRD-APP-BCPASSAYMGMT-DB")]
    //[Authorize(Roles = @"JNJ\JRD-APP-BCPASSAYMGMT-DB")]
    [AppAuthorizeAttribute(Roles = SystemRole.Administrators)]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Audit(string itemsPerPage, string pageno)
        {
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //List<AuditDTO> auditDTO = new List<AuditDTO>();
            //AdminBS adminBSobj = new AdminBS();

            //auditDTO = adminBSobj.GetAuditBS();

          //  ViewBag.AuditList = serializer.Serialize(auditDTO);

            return View();
        }

        [HttpGet]
        public ActionResult GetAuditList(string itemsPerPage, string pageno)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<AuditDTO> auditDTO = new List<AuditDTO>();
            AdminBS adminBSobj = new AdminBS();

            auditDTO = adminBSobj.GetAuditBS(itemsPerPage, pageno);

            return Json(auditDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ITSupport()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string logonUserName = string.Empty;
            string logonUserId = string.Empty;
            UserDetails userDetailsObj = new UserDetails();
            logonUserId = User.Identity.Name.ToString();
            userDetailsObj = LDAPinfoCollect.GetUserDetails(logonUserId);

            List<SupportApprovalDetailsDTO> myApprovalDetailsDTO = new List<SupportApprovalDetailsDTO>();
            AdminBS adminBSobj = new AdminBS();

            myApprovalDetailsDTO = adminBSobj.GetApprovalsBS(Convert.ToInt32(userDetailsObj.UserWWID), 0, "PendingApprovals");

            ViewBag.SupportApprovalList = serializer.Serialize(myApprovalDetailsDTO);

            return View("PendingSupportApprovals");
        }
    }
}