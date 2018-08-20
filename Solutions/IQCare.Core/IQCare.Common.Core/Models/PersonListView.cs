using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
   public  class PersonListView
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public int Sex { get; set; }
       
        public bool? DeleteFlag { get; set; }
        
        
        public DateTime? DateOfBirth { get; set; }
       
        public int? PersonIdentifier { get; set; }
        public string PersonIdentifierType { get; set; }

        public string PersonIdentifierValue { get; set; }
        public int? PatientIdentifier { get; set; }
        public string PatientIdentifierType { get; set; }

        public string PatientIdentifierValue { get; set; }

        public int? Patientid { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string EnrollmentNumber { get; set; }

    


    }
}
