using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Pharm.Core.Models
{
 public class PharmacyVisit
    {
        public int VisitID { get; set; }
        public string VisitName { get; set; }
        public int PatientId { get; set; }

        public DateTime? VisitDate { get; set; }

        public string UserName { get; set; }
        
        public string Status { get; set; }

        public string OrderStatusText { get; set; }


    }
}
