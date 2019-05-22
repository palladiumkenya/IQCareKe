using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PersonView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool DobPrecision { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int FacilityId { get; set; }
        public string NickName { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
