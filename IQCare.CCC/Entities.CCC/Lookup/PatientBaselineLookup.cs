using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("PatientBaselineView")]
    public class PatientBaselineLookup
    {
        [Key]
        public int Id { get; set; }
        public int patientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ServiceAreaId { get; set; }
        public DateTime TransferInDate { get; set; }
        public DateTime TreatmentStartDate { get; set; }
        public string CurrentTreatment { get; set; }
        public string CurrentTreatmentName { get; set; }
        public string FacilityFrom { get; set; }
        public int mflcode { get; set; }
        public string CountyFrom { get; set; }
        public string TransferInNotes { get; set; }
        public DateTime HivDiagnosisDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int EnrollmentWHOStage { get; set; }
        public string EnrollmentWHOStageName { get; set; }
        public DateTime ARTInitiationDate { get; set; }
        public DateTime DateStartedOnFirstline { get; set; }
        public string Cohort { get; set; }
        public int Regimen { get; set; }
        public decimal BaselineViralLoad { get; set; }
        public DateTime BaselineViralLoadDate { get; set; }
        public bool? HBVInfected { get; set; }
        public bool? Pregnant { get; set; }
        public bool? TBinfected { get; set; }
        public int? WHOStage { get; set; }
        public string WHOStageName { get; set; }
        public bool? BreastFeeding { get; set; }
        public decimal? CD4Count { get; set; }
        public decimal? MUAC { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public decimal? BMI { get; set; }

    }
}
