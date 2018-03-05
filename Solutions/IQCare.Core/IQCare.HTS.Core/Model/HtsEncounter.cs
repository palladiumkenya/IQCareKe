using System;
using System.ComponentModel.DataAnnotations.Schema;
using IQCare.SharedKernel.Model;

namespace IQCare.HTS.Core.Model
{
    [Table("HtsEncounter")]
    public class HtsEncounter : Entity<Int32>
    {
        public int PatientEncounterID { get; set; }
        public int EverTested { get; set; }
        public int NoOfMonthsSinceReTest { get; set; }
        public int SelfTestPastTwelveMonths { get; set; }
        public int TestedAs { get; set; }
        public int TestingStrategy { get; set; }
        public string EncounterRemarks { get; set; }
        public int FinalResultGiven { get; set; }
        public int CoupleDiscordant { get; set; }
        public int AcceptedPartnerListing { get; set; }
        public int ReasonDeclinePartnerListing { get; set; }
    }
}