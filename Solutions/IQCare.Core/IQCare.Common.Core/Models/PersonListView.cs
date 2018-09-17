using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace IQCare.Common.Core.Models
{
   public  class PersonListView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int Sex { get; set; }
        public string Gender { get; set; }
        public bool DeleteFlag { get; set; }
        public string IdentifierValue { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string MobileNumber { get; set; }
    }
}
