using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.BusinessDTOs
{
    public class RequestUserDetailsDTO
    {
        public RequestParamsDTO RequestParams { get; set; }
        public RequesterDTO RequesterDTO { get; set; }
        public ManagerDTO ManagerDTO { get; set; }
        public string ApplicationId { get; set; }
        public string RequestStateId { get; set; }
        public string RequestCreatedDt { get; set; }
        public string StatusInd { get; set; }
    }
}
