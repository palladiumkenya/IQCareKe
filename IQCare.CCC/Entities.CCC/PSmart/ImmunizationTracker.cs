using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.PSmart
{
    [Serializable][Table("ImmunizationTracker")]
    public class ImmunizationTracker
    {
        [Key]
        public int Id { get; set; }
        public int? PersonId { get; set; }
        public int PtnPk { get; set; }
        public string AntigenAdministered { get; set; }
        public DateTime? DateAdministered { get; set; } 
       public int FacilityMFLCode { get; set; }
       public string ProviderName { get; set; }
       public string ProviderId { get; set; }
        public string LotNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}