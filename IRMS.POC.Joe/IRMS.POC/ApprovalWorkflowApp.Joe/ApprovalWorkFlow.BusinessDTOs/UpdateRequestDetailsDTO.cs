﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.BusinessDTOs
{
    public class UpdateRequestDetailsDTO
    {
        public string Option { get; set; }
        public string UserName { get; set; }
        public string RequestNo { get; set; }
        public string Comment { get; set; }
        public string TaskId { get; set; }
    }
}