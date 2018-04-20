using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.BusinessDTOs
{
    public class RequestParamsDTO
    {
        public IDSSPEntityDTO IDSSPEntity { get; set; }
        public SASDDEntityDTO SASDDEntity { get; set; }
        public HPALMEntityDTO HPALMEntity { get; set; }
        public MKSIntegrityDTO MKSIntegrity { get; set; }
        public DMCCEntityDTO DMCCEntity { get; set; }
        public IDSDropZoneEntityDTO IDSDropZoneEntity { get; set; }
    }
}
