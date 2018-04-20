using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.DataAccessDTOs
{
    public class UpdateUserRequestDetailsDO
    {
        public string UserName { get; set; }
        public string RequestNo { get; set; }
        public string TaskId { get; set; }
    }
}
