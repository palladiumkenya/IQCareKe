using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Pharm.Core.Models
{
  public    class DrugPrescription
    {
        public string DrugId { get; set; }
        public string BatchId { get; set; }
        public string FreqId { get; set; }

        public string Dose { get; set; }
        public string DrugAbbr { get; set; }
        public string Morning { get; set; }
        public string Midday { get; set; }
        public string Evening { get; set; }
        public string Night { get; set; }
        public string Duration { get; set; }
        public string qtyPres { get; set; }
        public string qtyDisp { get; set; }
        public string prophylaxis { get; set; }
    }
}
