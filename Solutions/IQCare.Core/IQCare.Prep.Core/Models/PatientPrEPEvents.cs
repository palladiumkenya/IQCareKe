using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Core.Models
{
    public class PatientPrEPEvents
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientEncounterId { get; set; }

        public DateTime? DateRestarted { get; set; }

        
    }
}
