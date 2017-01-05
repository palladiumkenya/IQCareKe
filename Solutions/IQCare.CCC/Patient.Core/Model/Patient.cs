using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [Table("Patient")]

    public class Patient : BaseEntity
    {
        [Required]
        public int Ptn_Pk { get; set;}

        [Required]
        public int PersonId {get; set; }
        [ForeignKey("PersonId")]

        [Required]
        public int FacilityId { get; set; }

        [Required]
        public string PatientIndex { get; set; }

        [Required]
        public int IdentificationType { get;set;}

        [Required]
        public string IdentificationNo { get; set; }

        [Required]
        public bool Active { get; set; }
    }
   
}
