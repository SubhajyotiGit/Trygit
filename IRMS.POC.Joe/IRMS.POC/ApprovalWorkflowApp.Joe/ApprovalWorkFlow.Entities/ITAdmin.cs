using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.Entities
{
    [Table("tbl_AWF_AdminUsers")]
    public class ITAdmin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 UserId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        [Required]
        public string NTId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string FirstNm { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string LastNm { get; set; }
    }
}
