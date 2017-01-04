using System;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [Table("PatientEnrollment")]

    public class PatientEnrollment :BaseEntity
    {

        public int ServiceAreaId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string EnrollmentStatusId { get; set; }
        public bool TransferIn { get; set; }
        public bool CareEnded { get; set; }
    }
}
