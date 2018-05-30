using System;

namespace IQCare.Common.Core.Models
{
    public class HivTestTracker
    {
        public int Id { get; set; }

        public int Ptn_Pk { get; set; }

        public int PersonId { get; set; }

        public string DiagnosisMode { get; set; }

        public string TestEpisode { get; set; }

        public string Result { get; set; }

        public DateTime ResultDate { get; set; }

        public string ResultCategory { get; set; }

        public string MFLCode { get; set; }

        public string ProviderName { get; set; }

        public string ProviderId { get; set; }

        public bool DeleteFlag { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }
    }
}