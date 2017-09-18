using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IQCare.Web.HTS.Models
{
    [Serializable]
    public class Referral
    {
        [Key]
        public int Id { get; set; }
        public string SysUuid { get; set; }
        public Encounter Encounter { get; set; }
        public int EncounterId { get; set; }
        public Provider Provider { get; set; }
        public int ProviderId { get; set; }
        public int MflCode { get; set; }
        public string FacilityName { get; set; }
        public DateTime DatePromised { get; set; }
    }
}
