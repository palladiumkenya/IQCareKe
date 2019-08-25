using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
   public  class PersonVitalsView
    {
         
        public int Id { get; set; }

        public int PersonId { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public decimal? Weight { get; set; }

        public DateTime? VisitDate { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
