using System;

namespace IQCare.HTS.Core.Model
{
    public class HTSEncountersView
    {
        public Int64 RowID { get; set; }

        public int PatientId { get; set; }

        public DateTime EncounterDate { get; set; }

        public string Provider { get; set; }

        public string ResultOne { get; set; }

        public string ResultTwo { get; set; }

        public string FinalResult { get; set; }

        public string Consent { get; set; }

        public string PartnerListingConsent { get; set; }
    }
}