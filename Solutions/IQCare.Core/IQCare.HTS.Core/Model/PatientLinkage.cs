using System;

namespace IQCare.HTS.Core.Model
{
    public class PatientLinkage
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public DateTime LinkageDate { get; set; }
        public string CCCNumber { get; set; }
        public string Facility { get; set; }
        public bool Enrolled { get; set; }
        public int PatientId { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
        public string HealthWorker { get; set; }
        public string Cadre { get; set; }
        public DateTime? ArtStartDate { get; set; }
        public string Comments { get; set; }
    }
}