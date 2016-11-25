using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.PatientCore
{
    [Serializable]
    [Table("PatientVisitView")]
   public class PatientVisit
    {
         [Column("VisitId")]
        public int Id { get; set; }
       
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public string VisitName { get; set; }
        public int LocationId { get; set; }
        public DateTime VisitDate { get; set; }
        public int DataQuality { get; set; }
        public int Signature { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserId { get; set; }
        public Boolean DeleteFlag { get; set; }
    }
}
