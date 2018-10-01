using System;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Tanners
{
    [Serializable]
    [Table("PatientTannersStaging")]
    public class PatientTannersStaging: BaseEntity
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
        public DateTime TannersStagingDate { get; set; }
        public int BreastsGenitalsId { get; set; }
        public int PubicHairId { get; set; }
    }
}
