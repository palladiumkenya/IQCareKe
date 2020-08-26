using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Pharm.Core.Models
{
    public class PatientVisit
    {
        public int Visit_Id { get; set; }

        public int Ptn_pk { get; set; }

        public int LocationID { get; set; }


        public DateTime VisitDate { get; set; }

        public int VisitType { get; set; }

        public int? DataQuality { get; set; }
        public int UserID { get; set; }

       public DateTime CreateDate { get; set; }

        public int CreatedBy { get; set; }

        public int? DeleteFlag { get; set; }

        
    }
}
