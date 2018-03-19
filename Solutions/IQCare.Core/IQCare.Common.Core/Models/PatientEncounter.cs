using System;
using System.Xml.Serialization;

namespace IQCare.Common.Core.Models
{
    public class PatientEncounter
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int EncounterTypeId { get; set; }
        public int Status { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime EncounterStartTime { get; set; }
        public DateTime EncounterEndTime { get; set; }
        public int ServiceAreaId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        [XmlIgnore]
        public string AuditData { get; set; }
    }
}