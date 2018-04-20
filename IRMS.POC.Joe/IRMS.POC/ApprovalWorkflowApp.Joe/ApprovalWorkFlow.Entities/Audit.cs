using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.Entities
{
    [Table("tbl_AWF_Tran_Audits")]
    public class Audit
    {
        [Key]
        public Int32 AuditId { get; set; }

        [Required]
        public int RequestId { get; set; }
        [ForeignKey("RequestId")]
        public virtual Request request { get; set; }

        [Required]
        public Int32 UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User user { get; set; }

        public int? RequestStateId { get; set; }
        [ForeignKey("RequestStateId")]
        public virtual RequestStatus requestStatus { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Comment { get; set; }

        public DateTime CreatedDt { get; set; }
    }
}
