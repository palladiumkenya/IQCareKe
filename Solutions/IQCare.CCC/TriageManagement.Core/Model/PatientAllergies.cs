using System;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace TriageManagement.Core.Model
{
    [Table("PatientAllergy")]
    public class PatientAllergies :BaseEntity
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public string Allergen { get; set; }
        public string AllergyResponse { get; set; }
        public string AuditData { get; set; }
    }
}
