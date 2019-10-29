using System;

namespace IQCare.Common.Core.Models
{
    public class DuplicatePersonsPoco
    {
        public string MatchingEnrollmentNo { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PatientEnrollmentId { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public string MobileNumber { get; set; }

        public int Ptn_Pk { get; set; }

        public int PatientId { get; set; }

        public int PersonId { get; set; }

        public Int64 GroupingFilter { get; set; }
    }
}