using System;
using IQCare.SharedKernel.Model;

namespace IQCare.HTS.Core.Model
{
    public class FinalResult : Entity<Int32>
    {
        public int HtsEncounterID { get; set; }
        public int FirstRoundResult { get; set; }
        public int SecondRoundResult { get; set; }
        public int FinalComputedResult { get; set; }
    }
}