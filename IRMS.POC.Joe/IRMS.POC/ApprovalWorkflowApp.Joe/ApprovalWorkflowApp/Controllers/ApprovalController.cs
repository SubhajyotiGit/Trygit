using ApprovalWorkFlow.Business;
using ApprovalWorkFlow.BusinessDTOs;
using ApprovalWorkflowApp.Models;
using ApprovalWorkflowApp.UtilityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ApprovalWorkflowApp.Controllers
{
    public class ApprovalController : Controller
    {
        // GET: Approval
        public ActionResult PendingApprovals()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string logonUserName = string.Empty;
            string logonUserId = string.Empty;
            UserDetails userDetailsObj = new UserDetails();
            logonUserId = User.Identity.Name.ToString();
            userDetailsObj = LDAPinfoCollect.GetUserDetails(logonUserId);

            List<MyApprovalDetailsDTO> myApprovalDetailsDTO = new List<MyApprovalDetailsDTO>();
            GetApprovalBS getMyApprovalObj = new GetApprovalBS();

            myApprovalDetailsDTO = getMyApprovalObj.GetApprovalsBS(Convert.ToInt32(userDetailsObj.UserWWID), 0 ,"PendingApprovals");

            ViewBag.MyApprovalList = serializer.Serialize(myApprovalDetailsDTO);

            return View();
        }

        public ActionResult AllApprovals()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string logonUserId = string.Empty;
            UserDetails userDetailsObj = new UserDetails();
            logonUserId = User.Identity.Name.ToString();
            userDetailsObj = LDAPinfoCollect.GetUserDetails(logonUserId);

            List<MyApprovalDetailsDTO> myApprovalDetailsDTO = new List<MyApprovalDetailsDTO>();
            GetApprovalBS getMyApprovalObj = new GetApprovalBS();

            myApprovalDetailsDTO = getMyApprovalObj.GetApprovalsBS(Convert.ToInt32(userDetailsObj.UserWWID), 0, "All");

            ViewBag.MyApprovalList = serializer.Serialize(myApprovalDetailsDTO);

            return View();
        }

        public ActionResult GetRequest(int id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string logonUserId = string.Empty;
            UserDetails userDetailsObj = new UserDetails();
            logonUserId = User.Identity.Name.ToString();
            userDetailsObj = LDAPinfoCollect.GetUserDetails(logonUserId);

            List<MyApprovalDetailsDTO> myApprovalDetailsDTO = new List<MyApprovalDetailsDTO>();
            GetApprovalBS getMyApprovalObj = new GetApprovalBS();
          
            myApprovalDetailsDTO = getMyApprovalObj.GetApprovalsBS(Convert.ToInt32(userDetailsObj.UserWWID), id, "PendingApprovals");

            ViewBag.MyApprovalList = serializer.Serialize(myApprovalDetailsDTO);

            return View("PendingApprovals");
        }
        [HttpPost]
        public ActionResult UpdateRequest(ApprovalRequestDetailsVM approvalRequestDetailsVM)
        {
            string eightDigitNumber = string.Empty;
            GetApprovalBS getApprovalBSobj = new GetApprovalBS();

            UpdateRequestDetailsDTO UpdateRequestDetailsDTO = new UpdateRequestDetailsDTO {
                 Comment= approvalRequestDetailsVM.Comment,
                 Option= approvalRequestDetailsVM.Option,
                 RequestNo= approvalRequestDetailsVM.RequestNo,
                 UserName= approvalRequestDetailsVM.UserName
            };

            if (approvalRequestDetailsVM.Option=="Approve")
            {
                Random r = new Random();
                int randNum = r.Next(1000000);
                eightDigitNumber = randNum.ToString("D8");
                UpdateRequestDetailsDTO.TaskId = "TASK0000" + eightDigitNumber;
            }
            else
            {
                UpdateRequestDetailsDTO.TaskId = eightDigitNumber;
            }

            string status = getApprovalBSobj.UpdateRequest(UpdateRequestDetailsDTO);
            ApprovalStatus approvalStatusVM = new ApprovalStatus();
            if (status.ToUpper() == "UPDATED")
            {
                approvalStatusVM.Status = "UPDATED";
                approvalStatusVM.TaskId = "TASK0000" + eightDigitNumber;
            }
            else
            {
                approvalStatusVM.Status = "FAILED";
                approvalStatusVM.TaskId = eightDigitNumber;
            }

            
            return Json(approvalStatusVM, JsonRequestBehavior.AllowGet);
            
        }
    }
}