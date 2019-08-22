using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.Core.Model
{
    public class HtsEncounterResult
    {
        public int Id { get; set; }
        public int HtsEncounterId { get; set; }
        public int RoundOneTestResult { get; set; }
        public int? RoundTwoTestResult { get; set; }
        public int? FinalResult { get; set; }
        public string EncounterResultRemarks { get; set; }
        public int? SyphilisResult { get; set; }
    }
}
