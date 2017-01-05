using System;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;
using Microsoft.Build.Framework;

namespace TriageManagement.Core.Model
{
    [Table("PatientAllergy")]
    public class PatientAllergies :BaseEntity
    {
        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]

        [Required]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]

        [Required]
        public string Allergen { get; set; }

        [Required]
        public string AllergyResponse { get; set; }

        [Required]
        public string AuditData { get; set; }
    }
}
