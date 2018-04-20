using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkFlow.Entities
{
    [Table("tbl_AWF_Mstr_RequestStatus")]
    public class RequestStatus
    {
        [Key]
        public Int32 RequestStateId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string RequestNm { get; set; }

        public virtual List<Request> requests { get; set; }
    }
}
