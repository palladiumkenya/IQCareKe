using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Enrollment
{
    [Serializable]
    [Table("Patient")]
    public class PatientEntity : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public int? ptn_pk { get; set; }
        public string PatientIndex { get; set; }
        public int FacilityId { get; set; }
        public int PatientType { get; set; }
        public bool Active { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalId { get; set; }
        public virtual Person Person { get; set; }
        /*public virtual PatientArtInitiationBaseline PatientArtInitiationBaseline { get; set; }
        public virtual PatientArtUseHistory PatientArtUseHistory { get; set; }
        public virtual DiagnosisArvHistory PatientDiagnosis { get; set; }
        public virtual PatientHivEnrollmentBaseline PatientHivEnrollmentBaseline { get; set; }*/

    }
}
