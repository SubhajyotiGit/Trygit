using ApprovalWorkflowApp.UtilityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.DirectoryServices.AccountManagement;
using ApprovalWorkflowApp.Models;
using System.Net.Mail;
using ApprovalWorkFlow.Business;
using ApprovalWorkFlow.BusinessDTOs;
using System.IO;
using System.Web.UI;
using System.Text;

namespace ApprovalWorkflowApp.Controllers
{
    public class SubmitRequestController : Controller
    {
        // GET: SubmitRequest
        public ActionResult IDSSubmitRequest()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string logonUserName = string.Empty;
            string logonUserId = string.Empty;
            UserDetails userDetailsObj = new UserDetails();

            logonUserId = User.Identity.Name.ToString();
            userDetailsObj = LDAPinfoCollect.GetUserDetails(logonUserId);
            userDetailsObj.ApplicationId = "1";

            ViewBag.UserDetails = serializer.Serialize(userDetailsObj);
            return View();
        }
        [HttpPost]
        public ActionResult ValidateElectronicSign(UserModel userVM)
        {
            bool IsValidUser = false;
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                IsValidUser = context.ValidateCredentials(userVM.UserName, userVM.Password);
            }

            return Json(IsValidUser, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SubmitRequest(RequestDetailsVM requestDetailsVM)
        {
            bool IsRequestSubmitted = true;

            RequestBS requestBSObj = new RequestBS();
            RequestParamsDTO requestDTO = new RequestParamsDTO {
                IDSSPEntity = requestDetailsVM.IDSSPEntity,
                SASDDEntity = requestDetailsVM.SASDDEntity,
                HPALMEntity = requestDetailsVM.HPALMEntity,
                MKSIntegrity = requestDetailsVM.MKSIntegrity,
                DMCCEntity = requestDetailsVM.DMCCEntity,
                IDSDropZoneEntity = requestDetailsVM.IDSDropZoneEntity,
            };

            UserDTO requesterUserDTO = new UserDTO {
                 EmailId = requestDetailsVM.UserEmailId,
                 FirstName = requestDetailsVM.FirstName,
                 LastName =requestDetailsVM.LastName,
                 NTId = requestDetailsVM.NTId,
                 UserId = requestDetailsVM.UserId
            };
            RequesterDTO requesterDTO = new RequesterDTO { User = requesterUserDTO };
           
            UserDTO managerUserDTO = new UserDTO
            {
                EmailId = requestDetailsVM.ManagerEmailId,
                FirstName = requestDetailsVM.ManagerFirstName,
                LastName = requestDetailsVM.ManagerLastName,
                NTId = requestDetailsVM.ManagerNetworkId,
                UserId = requestDetailsVM.ManagerWWID
            };
            ManagerDTO managerDTO = new ManagerDTO { User = managerUserDTO };
            RequestUserDetailsDTO requestUserDetailsDTO = new RequestUserDetailsDTO {
                ApplicationId = requestDetailsVM.ApplicationId,
                ManagerDTO = managerDTO,
                RequesterDTO = requesterDTO,
                RequestCreatedDt = DateTime.Now.ToString(),
                RequestParams= requestDTO,
                RequestStateId = requestDetailsVM.RequestStateId,
                StatusInd="1"
            };

            int requestId = requestBSObj.SubmitRequestBS(requestUserDetailsDTO);
            if (requestId != 0)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(requestDetailsVM.UserEmailId);   
                    mail.To.Add(requestDetailsVM.ManagerEmailId);
                    SmtpClient client = new SmtpClient();
                    client.Port = 25;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new System.Net.NetworkCredential(requestDetailsVM.NTId, requestDetailsVM.Password);
                    client.Host = "smtp.na.jnj.com";
                    mail.Subject = "Request Id:"+requestId+"-Approval Request For" + requestDetailsVM.UserName;
                    mail.IsBodyHtml = true;

                  //  StringWriter writer = new StringWriter();
                  //  HtmlTextWriter html = new HtmlTextWriter(writer);

                  //  //html.RenderBeginTag(HtmlTextWriterTag.H1);
                  //  //html.WriteEncodedText("Heading Here");
                  //  //html.RenderEndTag();
                  //  html.WriteEncodedText(String.Format("Dear {0} {1}", requestDetailsVM.ManagerName,','));
                  //  html.WriteBreak();
                  //  html.RenderBeginTag(HtmlTextWriterTag.P);
                  //  html.WriteEncodedText("Please visit below link to make the approval");
                  //  html.RenderEndTag();
                  ////  html.WriteBreak();
                  //  html.AddAttribute(HtmlTextWriterAttribute.Href, "http://WIN-HRCH1I5K9R1.wks.jnj.com/Approval/GetRequest/" + requestId);
                  //  html.RenderBeginTag(HtmlTextWriterTag.A);
                  //  html.WriteEncodedText("Approval Link");
                  //  html.RenderEndTag();
                  ////  html.WriteBreak();
                  //  html.RenderBeginTag(HtmlTextWriterTag.P);
                  //  html.WriteEncodedText("Thanks and Regards,");
                  ////  html.RenderEndTag();
                  //  html.WriteBreak();
                  ////  html.RenderBeginTag(HtmlTextWriterTag.P);
                  //  html.WriteEncodedText("IDS Support Team");
                  //  html.RenderEndTag();
                  // // html.WriteBreak();
                  //  html.Flush();

                  //  string htmlString = writer.ToString();

                    ViewData.Add("RequestId", requestId);
                    ViewData.Add(new KeyValuePair<string, object>("Approver", requestDetailsVM.ManagerName));
                    ViewData.Add(new KeyValuePair<string, object>("Requester", requestDetailsVM.UserName));

                    string htmlEmailBody = HTMLHelper.ToHtml("ApprovalEmailTemplate", ViewData, this.ControllerContext).ToString();
                    mail.Body = htmlEmailBody.ToString();

                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    IsRequestSubmitted = false;
                    return Json(IsRequestSubmitted, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(IsRequestSubmitted, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, string user)
        {
            FileUploadViewModel fileUploadVM = new FileUploadViewModel();
            if (file != null && file.ContentLength > 0)
                try
                {
                    
                    string path = Path.Combine(Server.MapPath("~/Images"),
                                               Path.GetFileNameWithoutExtension(file.FileName)+ "_"+ user+ "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") +Path.GetExtension(file.FileName));
                    DirectoryInfo dir = new DirectoryInfo(HttpContext.Server.MapPath("~/Images/"));
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    file.SaveAs(path);
                    fileUploadVM = new FileUploadViewModel()
                    {
                        Message = "File uploaded successfully",
                        FilePath = path
                    };
                }
                catch (Exception ex)
                {
                    fileUploadVM = new FileUploadViewModel()
                    {
                        Message = "ERROR:" + ex.Message.ToString(),
                        FilePath = null
                    };
                }
            else
            {
                fileUploadVM = new FileUploadViewModel()
                {
                    Message = "You have not specified a file.",
                    FilePath = null
                };
            }
            return Json(fileUploadVM, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SearchManager(string managerNTId)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            UserDetails userDetailsObj = new UserDetails();

            userDetailsObj = LDAPinfoCollect.GetUserDetails(managerNTId);

            return Json(userDetailsObj, JsonRequestBehavior.AllowGet);
        }
    }
}