using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.Entities
{
    [Table("tbl_AWF_Users")]
    public class User : AuditColumns
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 UserId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string FirstNm { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string LastNm { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string EmailId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        [Required]
        public string NTId { get; set; }

        //public Int32? RoleId { get; set; }
        //[ForeignKey("RoleId")]
        //public virtual Role userRole { get; set; }

        [InverseProperty("User")]
        public List<Request> Users { get; set; }

        [InverseProperty("Manager")]
        public List<Request> Managers { get; set; }

        public virtual List<Audit> audits { get; set; }
    }
}
