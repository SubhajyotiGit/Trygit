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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string logonUserName = string.Empty;
            string logonUserId = string.Empty;
            UserDetails userDetailsObj = new UserDetails();
            logonUserId = User.Identity.Name.ToString();
            userDetailsObj = LDAPinfoCollect.GetUserDetails(logonUserId);

            GetMyRequestBS getMyRequestObj = new GetMyRequestBS();

            var myRequestDetailsDTO = getMyRequestObj.GetAllRequestBS(Convert.ToInt32(userDetailsObj.UserWWID));

            ViewBag.MyRequestList = serializer.Serialize(myRequestDetailsDTO);

            return View();
        }
        [HttpGet]
        public ActionResult GetStatus(string id)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            GetMyRequestBS getMyRequestObj = new GetMyRequestBS();
            List<RequestStatusDTO> requestStatusDTO = getMyRequestObj.GetStatus(id);

            ViewBag.RequestStatusList = serializer.Serialize(requestStatusDTO);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DownloadFile(string filepath, string filename)
        {
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = false,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
        }

        [HttpPost]
        public ActionResult UpdateRequest(UpdateRequestDetailsVM requestDetailsVM)
        {
            GetMyRequestBS getMyRequestObj = new GetMyRequestBS();
            UpdateUserRequestDetailsDTO UpdateRequestDetailsDTO = new UpdateUserRequestDetailsDTO
            {
                TaskId = requestDetailsVM.TaskId,
                RequestNo = requestDetailsVM.RequestNo,
                UserName = requestDetailsVM.UserName
            };
            string status = getMyRequestObj.UpdateRequest(UpdateRequestDetailsDTO);

            return Json(status, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}