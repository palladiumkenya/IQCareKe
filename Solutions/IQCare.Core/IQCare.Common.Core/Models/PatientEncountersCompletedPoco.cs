using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PatientEncountersCompletedPoco
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string EncounterType { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime EncounterStartTime { get; set; }
    }
}
