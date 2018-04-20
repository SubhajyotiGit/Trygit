using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApprovalWorkflowApp.Models
{
    public class ApprovalRequestDetailsVM
    {
        public string Option { get; set; }
        public string UserName { get; set; }
        public string RequestNo { get; set; }
        public string Comment { get; set; }
    }
}