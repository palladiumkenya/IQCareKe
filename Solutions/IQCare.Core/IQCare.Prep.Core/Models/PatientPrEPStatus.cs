using System;

namespace IQCare.Prep.Core.Models
{
    public class PatientPrEPStatus
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientEncounterId { get; set; }
        public int SignsOrSymptomsHIV { get; set; }
        public int AdherenceCounsellingDone { get; set; }
       // public int ContraindicationsPrepPresent { get; set; }
        public int PrepStatusToday { get; set; }
        public int? CondomsIssued { get; set; }
        public int? NoOfCondoms { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
    }
}