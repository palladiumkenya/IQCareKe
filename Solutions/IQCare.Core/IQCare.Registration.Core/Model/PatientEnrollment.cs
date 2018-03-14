using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace IQCare.Registration.Core.Model
{
    public class PatientEnrollment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ServiceAreaId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int EnrollmentStatusId { get; set; }
        public bool TransferIn { get; set; }
        public bool CareEnded { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; } 
    }
}
