using System;

namespace IQCare.PMTCT.Core.Models.HEI
{
    public class PatientIptOutcome
    {
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public bool IptEvent { get; set; }
        public string ReasonForDiscontinuation { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}