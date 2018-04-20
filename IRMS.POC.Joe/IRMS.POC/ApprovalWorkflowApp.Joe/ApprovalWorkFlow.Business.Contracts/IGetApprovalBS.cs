using ApprovalWorkFlow.BusinessDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.Business.Contracts
{
    public interface IGetApprovalBS
    {
        List<MyApprovalDetailsDTO> GetApprovalsBS(int userId, int requestId, string type);
        string UpdateRequest(UpdateRequestDetailsDTO updateRequestDetailsDTO);
    }
}
