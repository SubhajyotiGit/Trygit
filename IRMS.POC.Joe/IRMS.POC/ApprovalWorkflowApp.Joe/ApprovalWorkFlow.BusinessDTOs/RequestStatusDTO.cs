using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.BusinessDTOs
{
    public class RequestStatusDTO
    {
        public int? RequestId { get; set; }
        public int RequestStateId { get; set; }
        public string TransactionDate { get; set; }
        public string RequestStatusName { get; set; }
        public string Comment { get; set; }
    }
}
