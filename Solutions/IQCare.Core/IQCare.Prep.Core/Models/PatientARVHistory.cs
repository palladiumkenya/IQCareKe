using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Core.Models
{
    public class PatientARVHistory
    {

        public int Id { get; set; }

        public int PatientId { get; set; }


        public int PatientMasterVisitId { get; set; }
        public string TreatmentType { get; set; }
        public string Purpose { get; set; }
        public string Regimen { get; set; }
        public DateTime? DateLastUsed { get; set; }

        public Boolean DeleteFlag { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        // public int? Weeks { get; set; }

        public int? Months { get; set; }

        public DateTime? InitiationDate { get; set; }



    }
}
