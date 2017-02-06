using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("HIVTesting")]

    public class PatientHivTesting:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        [ForeignKey("PatientmasterVisit")]
        public int PatientMasterVisitId { get; set; }
         public int BaselineResult { get; set; }
        public DateTime BaselineDate { get; set; }
        public DateTime TestingDate { get; set; }
        public bool ReferredToCare { get; set; }
        public string CccNumber { get; set; }
        public int EnrollmentId { get; set; }

        public virtual Person Person { get; set; }
    }
}
