using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApprovalWorkflowApp.UtilityClass
{
    public class UserDetails
    {
        public string ApplicationId { get; set; }
        public string UserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserNetworkId { get; set; }
        public string UserEmailId { get; set; }
        public string UserWWID { get; set; }
        public string ManagerName { get; set; }
        public string ManagerEmailId { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public string ManagerNetworkId { get; set; }
        public string ManagerWWID { get; set; }
    }
}