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
        public int? PtnPk { get; set; }
        public string  EnrollmentNumber { get; set; }
        public string PatientIndex { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Sex { get; set; }
        public bool Active { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool PatientStatus { get; set; }
        public bool TransferIn { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalId { get; set; }
        public int FacilityId { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
