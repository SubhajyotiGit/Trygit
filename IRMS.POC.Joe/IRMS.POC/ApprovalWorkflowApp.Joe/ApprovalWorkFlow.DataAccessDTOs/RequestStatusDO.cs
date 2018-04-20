using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.DataAccessDTOs
{
    public class RequestStatusDO
    {
        public int? RequestId { get; set; }
        public int RequestStateId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string RequestStatusName { get; set; }
        public string Comment { get; set; }
    }
}
