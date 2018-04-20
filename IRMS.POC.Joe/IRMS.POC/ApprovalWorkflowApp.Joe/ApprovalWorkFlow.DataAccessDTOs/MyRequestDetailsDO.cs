using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.DataAccessDTOs
{
    public class MyRequestDetailsDO
    {
        public int RequestNo { get; set; }
        public string AppName { get; set; }
        public string RequestStatusName { get; set; }
        public string Comment { get; set; }
        public string Approver { get; set; }
        public string IrisTaskId { get; set; }
        public DateTime RequestCreateDate { get; set; }
        public DateTime? RequestCompleteDate { get; set; }
        public string RequestParameters { get; set; }
    }
}
