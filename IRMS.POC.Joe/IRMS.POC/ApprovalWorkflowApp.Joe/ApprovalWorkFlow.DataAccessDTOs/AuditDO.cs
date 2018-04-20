using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.DataAccessDTOs
{
    public class AuditDO
    {
        public Int64 RowNum { get; set; }
        public Int32 TotalCount { get; set; }
        public int AuditId { get; set; }
        public int RequestId { get; set; }
        public string UserName { get; set; }
        public string CreatedON { get; set; }
    }
}
