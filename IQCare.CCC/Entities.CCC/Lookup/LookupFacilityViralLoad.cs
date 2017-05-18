using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("Laboratory_ViralLoad")]
    public class LookupFacilityViralLoad
    {
        [Key]
        public int Id { get; set; }
        public int patientId { get; set; }
        public decimal ResultValues { get; set; }
        public int FacilityId { get; set; }
    }
}
