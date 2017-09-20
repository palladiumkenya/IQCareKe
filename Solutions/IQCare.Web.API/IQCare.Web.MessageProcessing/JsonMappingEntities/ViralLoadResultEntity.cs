using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Web.MessageProcessing.JsonMappingEntities
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
        public List<VIRALLOADRESULT> VIRAL_LOAD_RESULT { get; set; }
    }
}
