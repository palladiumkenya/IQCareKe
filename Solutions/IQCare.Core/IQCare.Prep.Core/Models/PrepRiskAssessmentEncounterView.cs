using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Core.Models
{
   public  class PrepRiskAssessmentEncounterView
    {
        public int Id { get; set; }
        public int PatientId { get; set; }

        public int PersonId { get; set; }
        public int EncounterTypeId { get; set; }

        public DateTime? VisitDate { get; set; }


        public int PatientMasterVisitId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string ServiceAreaName { get; set; }
        public int ServiceAreaId { get; set; }
      

       
       
        public Boolean DeleteFlag { get; set; }


        public string ClientWillingTakingPrep { get; set; }



    }
}
