using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.DataAccessDTOs
{
    public class RequestUserDetailsDO
    {
        public string RequestParams { get; set; }
        public RequesterDO RequesterDO { get; set; }
        public ManagerDO ManagerDO { get; set; }
        public string ApplicationId { get; set; }
        public string RequestStateId { get; set; }
        public string RequestCreatedDt { get; set; }
        public string StatusInd { get; set; }
    }
}
