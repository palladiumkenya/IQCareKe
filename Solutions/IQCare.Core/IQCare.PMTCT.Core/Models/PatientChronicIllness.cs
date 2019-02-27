using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Core.Models
{
    public class PatientChronicIllness
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ChronicIllness { get; set; }
        public string Treatment { get; set; }
        public int Dose { get; set; }
        public int Duration { get; set; }
        public bool DeleteFlag { get; set; }
        public DateTime OnsetDate { get; set; }
        public int Active { get; set; }
        public int CreateBy { get; set; }
    }
}
