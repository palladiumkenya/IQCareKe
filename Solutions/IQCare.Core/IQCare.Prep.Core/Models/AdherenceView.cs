using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Core.Models
{
    public class AdherenceView
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }

        public int Score { get; set; }

        public int AdherenceType { get; set; }

        public string AdherenceTypeName { get; set; }

        public string ScoreName { get; set; }

        public bool DeleteFlag { get; set; }

        public DateTime? VisitDate { get; set; }
    }
}
