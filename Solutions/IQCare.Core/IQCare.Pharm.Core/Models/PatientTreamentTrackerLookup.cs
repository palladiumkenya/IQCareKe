using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IQCare.Pharm.Core.Models
{
   public  class PatientTreamentTrackerLookup
    {
        
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ServiceAreaId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime? RegimenStartDate { get; set; }
        public int RegimenId { get; set; }
        public string Regimen { get; set; }
        public int RegimenLineId { get; set; }
        public string RegimenLine { get; set; }
        public int? DrugId { get; set; }
        public DateTime? RegimenStatusDate { get; set; }
        public int TreatmentStatusId { get; set; }
        public string TreatmentStatus { get; set; }
        public int TreatmentStatusReasonId { get; set; }
        public string TreatmentReason { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DispensedByDate { get; set; }
    }
}
