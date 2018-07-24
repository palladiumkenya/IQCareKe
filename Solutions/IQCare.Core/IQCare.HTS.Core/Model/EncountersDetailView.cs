using System;

namespace IQCare.HTS.Core.Model
{
    public class EncountersDetailView
    {
        public Int64 RowID { get; set; }
        public int PersonId { get; set; }

        public int EncounterId { get; set; }

        public int PatientEncounterId { get; set; }

        public int PatientId { get; set; }

        public DateTime EncounterDate { get; set; }
        public string EverSelfTested { get; set; }

        public string TestType { get; set; }

        public string Provider { get; set; }

        public string ResultOne { get; set; }

        public string ResultTwo { get; set; }

        public string FinalResult { get; set; }

        public string Consent { get; set; }

        public string PartnerListingConsent { get; set; }

        public string ServiceEntryPoint { get; set; }

        public string EverTested { get; set; }

        public int? MonthsSinceLastTest { get; set; }

        public string TestedAs { get; set; }

        public string CoupleDiscordant { get; set; }

        public string EncounterRemarks { get; set; }

        public string FinalResultGiven { get; set; }

        public string TestingStrategy { get; set; }
        public string TBScreening { get; set; }
        public string PartnerListingConsentDeclineReason { get; set; }
    }
}