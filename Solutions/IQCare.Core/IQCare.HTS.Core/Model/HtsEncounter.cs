using System;
using IQCare.SharedKernel.Model;

namespace IQCare.HTS.Core.Model
{
    public class HtsEncounter : Entity<Int32>
    {
        public int PatientEncounterID { get; set; }
        public int EverTested { get; set; }
        public int NoOfNumbersSinceReTest { get; set; }
        public int SelfTestPast12Months { get; set; }
        public int TestedAs { get; set; }
        public int  TestingStrategy { get; set; }
        public int FinalResultGiven { get; set; }
        public int CoupleDiscordant { get; set; }
        public int AcceptedPartnerListing { get; set; }
        public int ReasonDeclinePartnerListing { get; set; }
    }
}