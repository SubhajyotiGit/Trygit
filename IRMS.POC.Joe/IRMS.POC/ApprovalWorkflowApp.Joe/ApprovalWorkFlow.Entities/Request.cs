using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.Entities
{
    [Table("tbl_AWF_Tran_Requests")]
    public class Request : AuditColumns
    {
        [Key]
        public Int32 RequestId { get; set; }

        public int UserId { get; set; }
        public int ManagerId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("ManagerId")]
        public User Manager { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string TaskId { get; set; }

        [Required]
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public virtual Application application { get; set; }

        [Required]
        [Column(TypeName = "XML")]
        public string RequestParams { get; set; }

        public int RequestStateId { get; set; }
        [ForeignKey("RequestStateId")]
        public virtual RequestStatus requestStatus { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Comment { get; set; }

        [Required]
        public DateTime RequestCreatedDt { get; set; }

        public DateTime? RequestCompletedDt { get; set; }

        public virtual List<Audit> audits { get; set; }
    }
}
