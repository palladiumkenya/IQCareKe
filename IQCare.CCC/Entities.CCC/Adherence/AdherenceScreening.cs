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

namespace Entities.CCC.Adherence
{
    [Serializable]
    [Table("AdherenceScreening")]
    public class AdherenceScreening : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public double Total { get; set; }
        public string DepressionSeverity { get; set; }
        public string RecommendedManagement { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
    }
}
