using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PersonIdentifierView
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Sex { get; set; }
        public bool? Active { get; set; }
        public bool DeleteFlag { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public string AuditData { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? DobPrecision { get; set; }
        public string PersonIdentifier { get; set; }
        public string PersonIdentifierType { get; set; }

        public string PersonIdentifierValue { get; set; }
        public string PatientIdentifier { get; set; }
        public string PatientIdentifierType { get; set; }

        public string PatientIdentifierValue { get; set; }


      
    }
}
