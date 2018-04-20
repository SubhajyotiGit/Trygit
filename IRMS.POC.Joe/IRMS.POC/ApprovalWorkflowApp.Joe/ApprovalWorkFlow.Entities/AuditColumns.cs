using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.Entities
{
    public class AuditColumns
    {
        [Required]
        public bool StatusInd { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string CreatedById { get; set; }

        public DateTime? CreatedDt { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string ModifiedById { get; set; }

        public DateTime? ModifiedDt { get; set; }
    }
}
