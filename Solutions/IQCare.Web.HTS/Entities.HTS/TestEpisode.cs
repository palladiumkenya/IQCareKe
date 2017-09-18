using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.HTS
{
    [Serializable]
    public class TestEpisode
    {
        [Key]
        public int Id { get; set; }
        public string SysUuid { get; set; }
        public Encounter Encounter { get; set; }
        public int TestRound { get; set; }
        public int Outcome { get; set; }
    }
}
