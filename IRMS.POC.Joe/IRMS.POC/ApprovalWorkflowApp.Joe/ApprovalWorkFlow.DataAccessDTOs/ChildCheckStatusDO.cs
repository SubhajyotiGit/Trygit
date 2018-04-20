using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApprovalWorkFlow.DataAccessDTOs
{
    [XmlType(TypeName = "ChildCheckStatus")]
    public class ChildCheckStatusDO
    {
        public string AccessName { get; set; }
        public string CheckStatus { get; set; }
    }
}
