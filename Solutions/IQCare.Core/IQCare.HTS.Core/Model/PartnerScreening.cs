using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.Core.Model
{
    public class PartnerScreening
    {
        public int Id { get; set; }
        public DateTime ScreeningDate { get; set; }
        public int IPVScreeningDone { get; set; }
        public int PhysicallyHurt { get; set; }
        public int Threaten { get; set; }
        public int ForcedSexual { get; set; }
        public int IPVOutcome { get; set;}
        public string Occupation { get; set; }
        public string PnsRelationship { get; set; }
        public int LivingWithClient { get; set; }
        public int HivStatus { get; set; }
        public int PnsApproach { get; set; }
        public int Eligible { get; set; }
        public DateTime DateBooked { get; set; }
    }
}
