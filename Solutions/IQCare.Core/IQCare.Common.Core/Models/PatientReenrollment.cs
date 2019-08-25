using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PatientReenrollment
    {
        public int Id { get; set; }
       
         public int PatientId { get; set; }
        public DateTime ReenrollmentDate { get; set; }
        public bool DeleteFlag { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public string AuditData { get; set; }


    }
}
