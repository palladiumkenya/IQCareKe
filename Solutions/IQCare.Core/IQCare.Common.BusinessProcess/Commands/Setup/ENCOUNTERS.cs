using System.Collections.Generic;

namespace IQCare.Common.BusinessProcess.Commands.Setup
{
    public class ENCOUNTERS
    {
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public PRE_TEST PRE_TEST { get; set; }
        public HIV_TESTS HIV_TESTS { get; set; }
        public NewReferral REFERRAL { get; set; }
        public NewTracing TRACING { get; set; }
        public NewLinkage LINKAGE { get; set; }
    }

    public class NewLinkage
    {
        public string FACILITY { get; set; }
        public string HEALTH_WORKER { get; set; }
        public string CARDE { get; set; }
        public string DATE_ENROLLED { get; set; }
        public string CCC_NUMBER { get; set; }
        public string REMARKS { get; set; }
    }

    public class NewTracing
    {
        public string TRACING_DATE { get; set; }
        public string TRACING_MODE { get; set; }
        public string TRACING_OUTCOME { get; set; }
    }

    public class NewReferral
    {
        public string REFERRED_TO { get; set; }
        public string DATE_TO_BE_ENROLLED { get; set; }
    }

    public class HIV_TESTS
    {
        public List<NewTests> SCREENING { get; set; }
        public List<NewTests> CONFIRMATORY { get; set; }
        public SUMMARY SUMMARY { get; set; }
    }

    public class SUMMARY
    {
        public string SCREENING_RESULT { get; set; }
        public string CONFIRMATORY_RESULT { get; set; }
        public string FINAL_RESULT { get; set; }
        public string FINAL_RESULT_GIVEN { get; set; }
        public string COUPLE_DISCORDANT { get; set; }
        public string PNS_ACCEPTED { get; set; }
        public string PNS_DECLINE_REASON { get; set; }
        public string REMARKS { get; set; }
    }

    public class NewTests
    {
        public string KIT_TYPE { get; set; }
        public string KIT_OTHER { get; set; }
        public string LOT_NUMBER { get; set; }
        public string EXPIRY_DATE { get; set; }
        public string RESULT { get; set; }
    }

    public class PLACER_DETAIL
    {
        public int PROVIDER_ID { get; set; }
        public string ENCOUNTER_NUMBER { get; set; }
    }

    public class PRE_TEST
    {
        public int ENCOUNTER_TYPE { get; set; }
        public string ENCOUNTER_DATE { get; set; }
        public int SERVICE_POINT { get; set; }
        public int EVER_TESTED { get; set; }
        public int MONTHS_SINCE_LAST_TEST { get; set; }
        public int SELF_TEST_12_MONTHS { get; set; }
        public int DISABILITY_INDICATOR { get; set; }
        public List<int> DISABILITIES { get; set; }
        public int CONSENT { get; set; }
        public int TESTED_AS { get; set; }
        public int STRATEGY { get; set; }
        public int TB_SCREENING { get; set; }
        public string REMARKS { get; set; }
    }
}