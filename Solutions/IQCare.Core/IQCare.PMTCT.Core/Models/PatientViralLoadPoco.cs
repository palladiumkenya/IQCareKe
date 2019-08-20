using System;

namespace IQCare.PMTCT.Core.Models
{
    public class PatientViralLoadPoco
    {
        public int ptn_pk { get; set; }
        public DateTime orderdate { get; set; }
        public string orderstatus { get; set; }
        public int parameterid { get; set; }
        public decimal resultvalue { get; set; }
        public DateTime ResultDate { get; set; }
    }
}