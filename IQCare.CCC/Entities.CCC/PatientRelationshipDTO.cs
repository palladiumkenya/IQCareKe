using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC
{
    [Serializable]
    [Table("PatientRelationshipView")]
    public class PatientRelationshipDTO
    {
        public PatientRelationshipDTO()
        {
        }

        [Key]
        public int PatientId { get; set; }
        public int PatientPersonId { get; set; }
        public string PatientFirstName { get; set; }

        public string PatientMiddleName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientSex { get; set; }
        public DateTime? PatientDob { get; set; }
        public string RelativeFirstName { get; set; }
        public string RelativeMiddleName { get; set; }
        public string RelativeLastName { get; set; }
        public string RelativeSex { get; set; }
        public DateTime? RelativeDateOfBirth { get; set; }
        public string Relationship { get; set; }

    }
}
