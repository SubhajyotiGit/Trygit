using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.BusinessDTOs
{
    public class MyApprovalDetailsDTO
    {
        public int RequestNo { get; set; }
        public string AppName { get; set; }
        public string RequestStatus { get; set; }
        public string Comment { get; set; }
        public string Requester { get; set; }
        public string TaskId { get; set; }
        public string RequestCreateDT { get; set; }
        public string RequestCompleteDt { get; set; }
        public MyRequestParamsDTO RequestParam { get; set; }
    }
}
