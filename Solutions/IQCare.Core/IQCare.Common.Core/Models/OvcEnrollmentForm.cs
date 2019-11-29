using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class OvcEnrollmentForm
    {
        public int Id { get; set; }

        public int PersonId { get; set; }



        public string PartnerOVCServices { get; set; }

        public DateTime? EnrollmentDate { get; set; }



        public int CPMISEnrolled { get; set; }

        public int CreatedBy { get; set; }

        public bool DeleteFlag { get; set; }

        public DateTime CreateDate { get; set; }



    }
}
