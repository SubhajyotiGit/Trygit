using ApprovalWorkFlow.BusinessDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.Business.Contracts
{
    public interface IAdminBS
    {
        List<AuditDTO> GetAuditBS(string itemsPerPage, string pageno);
    }
}
