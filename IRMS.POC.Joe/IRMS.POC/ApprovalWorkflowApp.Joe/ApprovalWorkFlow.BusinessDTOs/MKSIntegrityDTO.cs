using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.BusinessDTOs
{
    public class MKSIntegrityDTO
    {
        public string AccessName { get; set; }
        public string CheckStatus { get; set; }

        public TrailName[] TrailNames { get; set; }
    }
}
