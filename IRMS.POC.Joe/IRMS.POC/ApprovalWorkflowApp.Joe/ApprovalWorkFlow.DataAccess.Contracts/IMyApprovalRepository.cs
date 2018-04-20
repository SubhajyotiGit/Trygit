using ApprovalWorkFlow.DataAccessDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.DataAccess.Contracts
{
    public interface IMyApprovalRepository
    {
        List<MyApprovalDetailsDO> GetApprovalDA(int userId, int requestId, string type);
        string UpdateRequestDA(UpdateRequestDetailsDO updateRequestDetailsDO);
    }
}
