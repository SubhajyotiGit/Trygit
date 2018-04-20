using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.Entities
{
    [Table("tbl_AWF_Roles")]
    public class Role : AuditColumns
    {
        [Key]
        public Int32 RoleId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string RoleNm { get; set; }
    }
}
