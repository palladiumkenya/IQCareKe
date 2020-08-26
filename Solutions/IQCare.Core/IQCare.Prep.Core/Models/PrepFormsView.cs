using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Core.Models
{
    public class PrepFormsView
    {
        public Int64 RowID { get; set; }
       public int EncounterId { get; set; }

        public int PatientId { get; set; }

        public string Form { get; set; }
        public int PatientMasterVisitId { get; set; }

        public DateTime? VisitDate { get; set; }

        public int? VisitType { get; set; }

        public string DisplayName { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public string AppointmentReason { get; set; }

        public string AssessmentOutCome { get; set; }
        public string ClientWillingTakingPrep { get; set; }

        public decimal? Weight { get; set; }

        public string PrepStatusToday { get; set; }

        public string SignsOrSymptomsHiv { get; set; }

        public string Contraindications { get; set; }
    }
}
