using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApprovalWorkFlow.BusinessDTOs
{
    public class ChildCheckStatus
    {
        public string AccessName { get; set; }
        public string CheckStatus { get; set; }
    }
}
