using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
   public class ContactsListView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public int?  PersonId { get; set; }

        public string Gender { get; set; }
        public string PhysicalAddress { get; set; }

        public string MobileNumber { get; set; }


        public string AlternativeNumber { get; set; }

        public string EmailAddress { get; set; }

        public string EnrollmentNumber { get; set; }

        public string PersonIdentificationNumber { get; set; }

        public bool? DeleteFlag { get; set; }
    }
}
