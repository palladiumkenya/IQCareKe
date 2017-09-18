using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IQCare.Web.HTS.Models
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
