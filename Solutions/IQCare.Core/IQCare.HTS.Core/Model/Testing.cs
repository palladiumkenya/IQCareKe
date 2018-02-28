using System;
using IQCare.SharedKernel.Model;

namespace IQCare.HTS.Core.Model
{
    public class Testing : Entity<Int32>
    {
        public int HtsEncounterID { get; set; }
        public int TestRound { get; set; }
        public int Kit { get; set; }
        public string LotNumber { get; set; }
        public DateTime DateOfExpiry { get; set; }
        public int TestResult { get; set; }
        public string OtherKit { get; set; }
    }
}