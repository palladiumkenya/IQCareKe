using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IQCare.Web.HTS.Models
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
