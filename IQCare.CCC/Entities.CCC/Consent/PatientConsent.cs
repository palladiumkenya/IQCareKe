using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.CCC.Consent
{
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