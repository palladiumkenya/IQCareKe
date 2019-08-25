using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Core.Models
{
   public  class AdherenceOutcome
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }

        public int Score { get; set; }

        public string ScoreDescription { get; set; }

        public bool DeleteFlag { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }

        public int AdherenceType { get; set; }
    }
}
