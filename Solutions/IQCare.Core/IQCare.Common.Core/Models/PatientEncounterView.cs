using System;
using System.ComponentModel.DataAnnotations;

namespace IQCare.Common.Core.Models
{
    public class PatientEncounterView
    {
        [Key]
        public int Id { get; set; }
        public int PatientEncounterId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int EncounterTypeId { get;set;}
        public DateTime EncounterStartTime { get; set; }
        public DateTime EncounterEndTime { get; set; }
        public int PatientId { get; set; }
        public int PersonId { get; set; }
        public int VisitNumber { get; set; }
        public string Encounter { get; set; }
        public string UserName { get; set; }
        public Boolean DeleteFlag { get; set; }
    }
}