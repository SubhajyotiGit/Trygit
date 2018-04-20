using ApprovalWorkFlow.BusinessDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApprovalWorkflowApp.Models
{
    public class RequestDetailsVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string NTId { get; set; }
        public string UserEmailId { get; set; }
        public string ManagerName { get; set; }
        public string ManagerEmailId { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public string ManagerNetworkId { get; set; }
        public string ManagerWWID { get; set; }
        public string ApplicationId { get; set; }
        public string RequestStateId { get; set; }
        public IDSSPEntityDTO IDSSPEntity { get; set; }
        public  SASDDEntityDTO SASDDEntity { get; set; }
        public  HPALMEntityDTO HPALMEntity { get; set; }
        public  MKSIntegrityDTO MKSIntegrity { get; set; }
        public  DMCCEntityDTO DMCCEntity { get; set; }
        public  IDSDropZoneEntityDTO IDSDropZoneEntity { get; set; }
    }
}