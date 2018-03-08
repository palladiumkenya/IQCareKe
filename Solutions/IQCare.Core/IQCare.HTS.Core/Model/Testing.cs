using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.Core.Model
{
    public class Testing
    {
        public int Id { get; set; }
        public int HtsEncounterId { get; set; }
        public int ProviderId { get; set; }
        public int KitId { get; set; }
        public string KitLotNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Outcome { get; set; }
        public int TestRound { get; set; }
    }
}
