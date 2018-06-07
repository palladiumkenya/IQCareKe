using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("gcPatientView")]
    public class PatientLookup
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int? ptn_pk { get; set; }
        public string  EnrollmentNumber { get; set; }
        public string PatientIndex { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Sex { get; set; }
        public bool Active { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool? TransferIn { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool DobPrecision { get; set; }
        public string NationalId { get; set; }
        public int FacilityId { get; set; }
        public int PatientType { get; set; }
        public string PatientStatus { get; set; }
        public string MobileNumber { get; set; }
        public int TBStatus { get; set; }
        public int NutritionStatus { get; set; }
        public int categorization { get; set; }
    }
    [Serializable]
    [Table("PersonExtView")]
    public class PersonExtLookup
    {
       
       
       [Key]
        public int PersonId { get; set; }
        public int? ptn_pk { get; set; }
        public string EnrollmentNumber { get; set; }
        public string PatientIndex { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Sex { get; set; }
        public bool Active { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool? TransferIn { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool DobPrecision { get; set; }
        public string NationalId { get; set; }
        public int FacilityId { get; set; }
        public int PatientType { get; set; }
        public string PatientStatus { get; set; }
        public string MobileNumber { get; set; }
        public int TBStatus { get; set; }
        public int NutritionStatus { get; set; }
        public int categorization { get; set; }
        public int Id { get; set; }
    }
}
