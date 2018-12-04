using System;
using System.Xml.Serialization;

namespace IQCare.Common.Core.Models
{
    public class PatientConsent
    {
        public int Id { get; set; }
        public int? PatientMasterVisitId { get; set; }
        public int? PatientId { get; set; }
        public int ServiceAreaId { get; set; }
        public int ConsentType { get; set; }
        public int ConsentValue { get; set; }
        public DateTime ConsentDate { get; set; }
        public int? DeclineReason { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        [XmlIgnore]
        public string AuditData { get; set; }

        public int? PersonId { get; set; }
        public string Comments { get; set; }
    }
}