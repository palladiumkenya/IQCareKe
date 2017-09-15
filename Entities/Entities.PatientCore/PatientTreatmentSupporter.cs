using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.PatientCore
{
    [Serializable]
    [Table("PatientTreatmentSupporter")]
    public class PatientTreatmentSupporter :  BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int? PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
        public int SupporterId {get;set;}
        public string MobileContact { get; set; }
    }
}
