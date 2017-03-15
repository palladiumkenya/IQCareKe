using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Consent
{
    [Serializable]
    [Table("PatientConsent")]
    public class PatientConsent : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public int ServiceAreaId { get; set; }
        public int ConsentType { get; set; }
        public DateTime ConsentDate { get; set; }
        public string DeclineReason { get; set; }
    }
}