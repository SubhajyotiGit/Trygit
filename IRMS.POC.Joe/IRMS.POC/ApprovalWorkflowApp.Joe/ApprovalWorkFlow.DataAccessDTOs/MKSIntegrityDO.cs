using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApprovalWorkFlow.DataAccessDTOs
{
    [XmlRoot("MKSIntegrity")]
    public class MKSIntegrityDO
    {
        public string AccessName { get; set; }
        public string CheckStatus { get; set; }
        public TrailNameDO[] TrailNames { get; set; }
    }
}
