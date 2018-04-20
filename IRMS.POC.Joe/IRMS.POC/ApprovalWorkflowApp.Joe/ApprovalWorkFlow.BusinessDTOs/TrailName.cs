using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApprovalWorkFlow.BusinessDTOs
{
    [XmlRoot("TrailName")]
    public class TrailName
    {
        public string text { get; set; }
    }
}
