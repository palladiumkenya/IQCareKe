using System;
using System.Xml.Serialization;

namespace IQCare.Common.Core.Models
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
        [XmlIgnore]
        public string AuditData { get; set; }
    }
}