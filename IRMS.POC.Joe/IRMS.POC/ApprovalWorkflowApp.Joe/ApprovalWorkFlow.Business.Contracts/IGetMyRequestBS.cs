using ApprovalWorkFlow.BusinessDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.Business.Contracts
{
    public interface IGetMyRequestBS
    {
        List<MyRequestDetailsDTO> GetAllRequestBS(int userId);
        List<RequestStatusDTO> GetStatus(string id);
    }
}
