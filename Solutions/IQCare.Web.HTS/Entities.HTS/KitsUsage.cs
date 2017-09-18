using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.HTS
{
    [Serializable]
    public class KitsUsage
    {
        [Key]
        public int Id { get; set; }
        public int TestEpisode { get; set; }
        public string KitName { get; set; }
        public string KitLotNumber { get; set; }
        public DateTime KitExpiryDate { get; set; }
        public int Outcome { get; set; }
    }
}
