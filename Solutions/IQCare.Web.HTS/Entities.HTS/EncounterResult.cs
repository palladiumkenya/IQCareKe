using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.HTS
{
    [Serializable]
    public class EncounterResult
    {
        [Key]
        public int Id { get; set; }
        public string SysUuid { get; set; }
        public Encounter Encounter { get; set; }
        public int EncounterId { get; set; }
        public Provider Provider { get; set; }
        public int ProviderId { get; set; }
        public int OutcomeGiven { get; set; }
        public int CoupleDiscordant { get; set; }
        public  int TbScreenStatus { get; set; }

    }
}
