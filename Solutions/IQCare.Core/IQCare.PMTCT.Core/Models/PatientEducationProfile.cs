using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Core.Models
{
    public class PatientEducationProfile
    {
        public int patientId { get; set; }
        public int patientMasterVisitId { get; set; }
        public int breastExamDone { get; set; }
        public List<PatientEducation> patientEducation { get; set; }
        public int treatedSyphilis { get; set; }
    }
}
