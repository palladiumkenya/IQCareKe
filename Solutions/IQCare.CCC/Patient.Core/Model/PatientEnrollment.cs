using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [Table("PatientEnrollment")]

    public class PatientEnrollment :BaseEntity
    {
        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]

        [Required]
        public int ServiceAreaId { get; set; }
        [ForeignKey("ServiceAreaId")]

        [Required]
        public DateTime EnrollmentDate { get; set; }

        [Required]
        public string EnrollmentStatusId { get; set; }
        [ForeignKey("EnrollmentStatusId")]

        [Required]
        public bool TransferIn { get; set; }

        [Required]
        public bool CareEnded { get; set; }
    }
}
