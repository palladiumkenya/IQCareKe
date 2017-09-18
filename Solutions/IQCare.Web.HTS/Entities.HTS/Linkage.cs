using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.HTS
{
    [Serializable]
    public class Linkage
    {
        [Key]
        public int Id { get; set; }
        public string SysUuid { get; set; }
        public Encounter Encounter { get; set; }
        public int EncounterId { get; set; }
        public string FacilityName { get; set; }
        public int MflCOde { get; set; }
        public string CareGiverName { get; set; }
        public DateTime DateOfLinkage { get; set; }
        public Provider Provider { get; set; }
        public int ProviderId { get; set; } 
        public string CccNumber { get; set; }
    }
}
