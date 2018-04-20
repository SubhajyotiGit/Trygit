using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.DataAccessDTOs
{
    public class HPALMEntityDO
    {
        public string AccessName { get; set; }
        public string CheckStatus { get; set; }

        public List<ChildCheckStatusDO> ChildCheckStatusList { get; set; }
    }
}
