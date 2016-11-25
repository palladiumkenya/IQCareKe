using Entities.Administration;
using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.PatientCore
{
    [Table("dtl_PatientAlert")]
    [Serializable]
    public class PatientAlert
    {
       [Key, Column(Order =0)] public int QueryId { get; set; }
      
        [Key, Column("ModuleId",Order = 1)]
        [ForeignKey("ServiceArea")]
        public int ServiceAreaId { get; set; }
        public virtual ServiceArea ServiceArea { get; set; }
        public virtual QueryDefinition Query { get; set; }
     
        public decimal OrdRank { get; set; }
    }
}
