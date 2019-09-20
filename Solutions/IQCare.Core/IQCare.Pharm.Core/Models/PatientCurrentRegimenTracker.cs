using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Pharm.Core.Models
{
    public class PatientCurrentRegimenTracker
    {
        public int RegimenId { get; set; }

        public int? RegimenLineId { get; set; }

        public DateTime?  VisitDate { get; set; }
    }
}
