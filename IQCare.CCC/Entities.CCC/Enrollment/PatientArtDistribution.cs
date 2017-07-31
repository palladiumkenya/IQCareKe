using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Enrollment
{
    [Serializable]
    [Table("PatientEnrollment")]
    public class PatientArtDistribution : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        public string ArtRefillModel { get; set; }
        public bool MissedArvDoses { get; set; }
        public int MissedArvDosesCount { get; set; }
        public bool Fatigue { get; set; }
        public bool Fever { get; set; }
        public bool Nausea { get; set; }
        public bool Diarrhea { get; set; }
        public bool Cough { get; set; }
        public bool Rash { get; set; }
        public bool GenitalSore { get; set; }
        public bool OtherSymptom { get; set; }
        public bool NewMedication { get; set; }
        public string NewMedicationText { get; set; }
        public bool FamilyPlanning { get; set; }
        public string FamilyPlanningMethod { get; set; }
        public bool ReferedToClinic { get; set; }
        public DateTime? ReferedToClinicDate { get; set; }
        public int PregnancyStatus { get; set; }
    }
}