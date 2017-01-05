using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.PatientCore
{
    [Serializable]
    public class PatientAllergy : IAuditEntity
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        public string Allergen { get; set; }
        public string AllergyResponse { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public bool DeleteFlag { get; set; }

        public string AuditData { get; set; }
    }
}
