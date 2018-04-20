using ApprovalWorkFlow.DataAccessDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.DataAccess.Contracts
{
    public interface IAdminRepository
    {
        List<AuditDO> GetAuditDA(string itemsPerPage, string pageno);
    }
}
