using System;
using System.Collections.Generic;

namespace IQCare.WebApi.Logic.MappingEntities
{
    public class VLPATIENTIDENTIFICATION
    {
        public EXTERNALPATIENTID EXTERNAL_PATIENT_ID { get; set; }
        public List<INTERNALPATIENTID> INTERNAL_PATIENT_ID { get; set; }
        public PATIENTNAME PATIENT_NAME { get; set; }
    }
    public class VIRALLOADRESULT
    {
        public DateTime DATE_SAMPLE_COLLECTED { get; set; }
        public DateTime DATE_SAMPLE_TESTED { get; set; }
        public DateTime SAMPLE_RESULT_DATE { get; set; }
        public string VL_RESULT { get; set; }
        public string SAMPLE_TYPE { get; set; }
        public string JUSTIFICATION { get; set; }
        public string REGIMEN { get; set; }
        public string LAB_TESTED_IN { get; set; }
    }

    public class ViralLoadResultEntity
    {
        public MESSAGEHEADER MESSAGE_HEADER { get; set; }
        public VLPATIENTIDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public List<VIRALLOADRESULT> VIRAL_LOAD_RESULT { get; set; }
    }
}
