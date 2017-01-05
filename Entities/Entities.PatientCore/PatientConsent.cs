using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.PatientCore
{
    [Serializable]
    public class PatientConsent : IAuditEntity
    {
        public string AuditData
        {
            get; set;
        }
        public DateTime CreateDate
        {
            get; set;
        }
        public int CreatedBy
        {
            get; set;
        }
        public bool DeleteFlag
        {
            get; set;
        }
        public int Id {get;set;}
        [ForeignKey("Patient")]
        public int PatientId { get; set; }      
        public Patient Patient { get; set; }
        public bool ConsentFlag { get; set; }
        public DateTime? DateOfConsent { get; set; }
        public string DeclineReason { get; set; }
        [ForeignKey("Consent")]
        public int ConsentId { get; set; }
        public Consent Consent { get; set; }
    }
}
