using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IQCare.Common.BusinessProcess.Commands.Setup
{
    public class ENCOUNTERS
    {
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public PRE_TEST PRE_TEST { get; set; }
        public HIV_TESTS HIV_TESTS { get; set; }
        public NewReferral REFERRAL { get; set; }
        public List<NewTracing> TRACING { get; set; }
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
        public string ARTStartDate { get; set; }
    }

    public class NewTracing
    {
        public string TRACING_DATE { get; set; }
        public int TRACING_MODE { get; set; }
        public int TRACING_OUTCOME { get; set; }
        public int? REASONNOTCONTACTED { get; set; }
        public string REASONNOTCONTACTEDOTHER { get; set; }
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
        public int SCREENING_RESULT { get; set; }
        public int CONFIRMATORY_RESULT { get; set; }
        public int FINAL_RESULT { get; set; }
        public int FINAL_RESULT_GIVEN { get; set; }
        public int COUPLE_DISCORDANT { get; set; }
        public int PNS_ACCEPTED { get; set; }
        public int PNS_DECLINE_REASON { get; set; }
        public string REMARKS { get; set; }
    }

    public class NewTests
    {
        public int KIT_TYPE { get; set; }
        public string KIT_OTHER { get; set; }
        public string LOT_NUMBER { get; set; }
        public string EXPIRY_DATE { get; set; }
        public int RESULT { get; set; }
        public int TEST_ROUND { get; set; }
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

    public class PARTNER_ENCOUNTER
    {
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public PARTNER_SCREENING PARTNER_SCREENING { get; set; }
        public List<PARTNER_TRACING> TRACING { get; set; }
    }

    public class PARTNER_SCREENING_ENCOUNTER
    {
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public PARTNER_SCREENING PARTNER_SCREENING { get; set; }
    }

    public class PARTNER_TRACING_ENCOUNTER
    {
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public List<PARTNER_TRACING> TRACING { get; set; }
    }

    public class PARTNER_TRACING
    {
        public string TRACING_DATE { get; set; }

        public int TRACING_MODE { get; set; }

        public int TRACING_OUTCOME { get; set; }

        public int CONSENT { get; set; }

        public string BOOKING_DATE { get; set; }
        public int? REASONNOTCONTACTED { get; set; }
        public string REASONNOTCONTACTEDOTHER { get; set; }
    }

    public class PARTNER_SCREENING
    {
        public int PNS_ACCEPTED { get; set; }

        public string SCREENING_DATE { get; set; }

        public int IPV_SCREENING_DONE { get; set; }

        public int HURT_BY_PARTNER { get; set; }

        public int THREAT_BY_PARTNER { get; set; }

        public int SEXUAL_ABUSE_BY_PARTNER { get; set; }

        public int IPV_OUTCOME { get; set; }

        public string PARTNER_OCCUPATION { get; set; }

        public int PARTNER_RELATIONSHIP { get; set; }

        public int LIVING_WITH_CLIENT { get; set; }

        public int HIV_STATUS { get; set; }

        public int PNS_APPROACH { get; set; }

        public int ELIGIBLE_FOR_HTS { get; set; }

        public string BOOKING_DATE { get; set; }
    }

    public class FAMILY_ENCOUNTER
    {
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public FAMILY_SCREENING FAMILY_SCREENING { get; set; }
        public List<FAMILY_TRACING> TRACING { get; set; }
    }

    public class FAMILY_SCREENING_ENCOUNTER
    {
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public FAMILY_SCREENING FAMILY_SCREENING { get; set; }
    }

    public class FAMILY_TRACING_ENCOUNTER
    {
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public List<FAMILY_TRACING> TRACING { get; set; }
    }

    public class FAMILY_TRACING
    {
        public string TRACING_DATE { get; set; }
        public string REMINDER_DATE { get; set; }
        public int TRACING_MODE { get; set; }
        public int TRACING_OUTCOME { get; set; }
        public int CONSENT { get; set; }
        public string BOOKING_DATE { get; set; }
        public int? REASONNOTCONTACTED { get; set; }
        public string REASONNOTCONTACTEDOTHER { get; set; }
    }

    public class FAMILY_SCREENING
    {
        public string SCREENING_DATE { get; set; }
        public int HIV_STATUS { get; set; }
        public int ELIGIBLE_FOR_HTS { get; set; }
        public string BOOKING_DATE { get; set; }
        public string REMARKS { get; set; }
    }
}