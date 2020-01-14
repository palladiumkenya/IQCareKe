using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Core.Models
{
   public  class ServiceCheckin
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        
        public int? EMRType { get; set; }
        public DateTime? VisitDate { get; set; }
        public bool Active { get; set; }
        
        public int? Status { get; set; }

        public bool DeleteFlag { get; set; }

        public int CreatedBy { get; set; }
    }
}
