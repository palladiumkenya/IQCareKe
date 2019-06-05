using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Core.Models
{
    public class RiskAssessment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int PatientMasterVisitId { get; set; }


        public int RiskAssessmentId { get; set; }
    
        public int RiskAssessmentValue { get; set; }

        public bool Active { get; set; }

        public string Comment { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public Boolean DeleteFlag { get; set; }

        public string AuditData { get; set; }
    }
}
