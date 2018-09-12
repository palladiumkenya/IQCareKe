using System;
using System.ComponentModel.DataAnnotations.Schema;
using IQCare.SharedKernel.Model;

namespace IQCare.HTS.Core.Model
{
    public class HtsEncounter 
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ProviderId { get; set; }
        public int PatientEncounterID { get; set; }
        public int? EverTested { get; set; }
        public int? MonthsSinceLastTest { get; set; }
        public int? MonthSinceSelfTest { get; set; }
        public int? TestedAs { get; set; }
        public int? TestingStrategy { get; set; }
        public string EncounterRemarks { get; set; }
        public int? FinalResultGiven { get; set; }
        public int? CoupleDiscordant { get; set; }
        public int TestEntryPoint { get; set; }
        //public int Consent { get; set; }
        public int? EverSelfTested { get; set; }
        public string GeoLocation { get; set; }
        public int EncounterType { get; set; }
    }
}