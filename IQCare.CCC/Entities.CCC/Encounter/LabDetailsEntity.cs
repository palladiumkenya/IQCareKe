using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Entities.Common;

namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("dtl_LabOrderTest")]
    public class LabDetailsEntity : BaseEntity
    {
        [Column]

        [Key]
        
            public int Id { get; set; }
            public int LabOrderId { get; set; }        
            public int LabTestId { get; set; }
            public string TestNotes { get; set; }
            public int IsParent { get; set; }
            public int? ParentTestId { get; set; }
            public string ResultNotes { get; set; }
            public int? ResultBy { get; set; }
            public DateTime? ResultDate { get; set; }
            public string ResultStatus { get; set; }
            public int UserId { get; set; }
            public DateTime? StatusDate { get; set; }
           



    }
    }
