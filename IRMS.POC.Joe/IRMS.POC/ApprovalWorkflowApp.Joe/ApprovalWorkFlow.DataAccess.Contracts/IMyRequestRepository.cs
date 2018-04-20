﻿using ApprovalWorkFlow.DataAccessDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.DataAccess.Contracts
{
    public interface IMyRequestRepository
    {
        List<MyRequestDetailsDO> GetAllRequestDA(int userId);
        List<RequestStatusDO> GetStatus(string id);
    }
}