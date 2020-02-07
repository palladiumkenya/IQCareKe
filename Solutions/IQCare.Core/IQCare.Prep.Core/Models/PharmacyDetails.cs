using IQCare.Pharm.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Core.Models
{
   public class PharmacyDetails
    {
        public string TreatmentProgram { get; set; }
        public string TreatmentPlan { get; set; }
        public string Reason { get; set; }

        public string Regimentext { get; set; }
        public string Regimen { get; set; }

        public string RegimenLine { get; set; }

        public string Period { get; set; }
     
         public List<DrugPrescription> DrugPrescriptions { get; set; }

     }

   
}
