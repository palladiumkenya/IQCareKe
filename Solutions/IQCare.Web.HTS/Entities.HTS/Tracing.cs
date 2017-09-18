using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.HTS
{
    [Serializable]
    public class Tracing
    {
        [Key]
        public int Id { get; set; }
        public string SysUuid { get; set; }
        public Provider Provider { get; set; }
        public int ProviderId { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public Encounter Encounter { get; set; }
        public int EncounterId { get; set; }
        public int TraceMethod { get; set; }
        public DateTime TraceDate { get; set; }
        public int Outcome { get; set; }
        public string Remarks { get; set; }
    }
}
