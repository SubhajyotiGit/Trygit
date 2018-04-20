using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.BusinessDTOs
{
    public class AuditDTO
    {
        public string RowNum { get; set; }
        public string TotalCount { get; set; }
        public int AuditId { get; set; }
        public int RequestId { get; set; }
        public string UserName { get; set; }
        public string CreatedON { get; set; }
    }
}
