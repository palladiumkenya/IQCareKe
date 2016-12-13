using System;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [System.Data.Linq.Mapping.Table(Name = "PatientEnrollment")]

    class PatientEnrollment :BaseEntity
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int ModuleId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string EnrollmentStatus { get; set; }
        public DateTime OutcomeDate { get; set; }
        public string OutCome { get; set; }
    }
}
