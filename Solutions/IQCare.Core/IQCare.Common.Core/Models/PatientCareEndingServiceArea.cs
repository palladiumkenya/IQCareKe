using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PatientCareEndingServiceArea
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

         
        public DateTime EnrollmentDate { get; set; }

        public int PatientId { get; set; }

        public int ServiceAreaId { get; set; }

        public DateTime ExitDate { get; set; }

        public string ExitReason { get; set; }


    }
}
