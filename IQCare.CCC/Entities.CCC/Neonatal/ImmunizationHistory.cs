using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Neonatal
{
    [Serializable]
    [Table("ImmunizationHistory")]
    public class ImmunizationHistory
    {
        [Column]
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public int CreatedBy { get; set; }
        public int ImmunizationPeriod { get; set; }
        public int ImmunizationGiven { get; set; }
        public DateTime ImmunizationDate { get; set; }
    }
}
