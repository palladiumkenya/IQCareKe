using System;

namespace IQCare.Common.Core.Models
{
    public class PersonDetailsView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string NickName { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string EducationLevel { get; set; }
        public string Occupation { get; set; }
        public string County { get; set; }
        public string SubCounty { get; set; }
        public string Ward { get; set; }
        public string Village { get; set; }
        public string NearestHealthCentre { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? DobPrecision { get; set; }
        public string MobileNumber { get; set; }
        public string AlternativeNumber { get; set; }
        public string EmailAddress { get; set; }

        public int? Sex { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? EducationLevelId { get; set; }
        public int? OccupationId { get; set; }
        public int? CountyId { get; set; }
        public int? SubCountyId { get; set; }
        public int? WardId { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}