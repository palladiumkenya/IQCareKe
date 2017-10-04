using System;
using System.Collections.Generic;

namespace IQCare.WebApi.Logic.MappingEntities
{
    public class VIRALLOADRESULT
    {
        public DateTime DATE_SAMPLE_COLLECTED { get; set; }
        public DateTime DATE_SAMPLE_TESTED { get; set; }
        public string VL_RESULT { get; set; }
        public string SAMPLE_TYPE { get; set; }
        public string JUSTIFICATION { get; set; }
        public string REGIMEN { get; set; }
        public string LAB_TESTED_IN { get; set; }
    }

    public class ViralLoadResultEntity
    {
        public MESSAGEHEADER MESSAGE_HEADER { get; set; }
        public PATIENTIDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public VIRALLOADRESULT VIRAL_LOAD_RESULT { get; set; }
    }
}
