using System;

namespace IQCare.Common.Core.Models
{
    public class PatientAllergyView
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public string Allergen { get; set; }
        public string AllergenName { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int Reaction { get; set; }
        public string ReactionName { get; set; }
        public int Severity { get; set; }
        public string SeverityName { get; set; }
        public DateTime OnsetDate { get; set; }
    }
}