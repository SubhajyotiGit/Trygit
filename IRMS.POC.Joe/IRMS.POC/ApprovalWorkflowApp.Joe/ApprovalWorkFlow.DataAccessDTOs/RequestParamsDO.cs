using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApprovalWorkFlow.DataAccessDTOs
{
    [XmlRoot("RequestParams")]
    public class RequestParamsDO
    {
        public IDSSPEntityDO IDSSPEntity { get; set; }
        public SASDDEntityDO SASDDEntity { get; set; }
        public HPALMEntityDO HPALMEntity { get; set; }
        public MKSIntegrityDO MKSIntegrity { get; set; }
        public DMCCEntityDO DMCCEntity { get; set; }
        public IDSDropZoneEntityDO IDSDropZoneEntity { get; set; }
    }
}
