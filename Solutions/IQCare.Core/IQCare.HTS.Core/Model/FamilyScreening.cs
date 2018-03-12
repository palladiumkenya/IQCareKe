using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.Core.Model
{
    public class FamilyScreening
    {
        public int Id { get; set; }
        public DateTime ScreeningDate { get; set; }
        public int HivStatus { get; set; }
        public int Eligible { get; set; }
        public DateTime DateBooked { get; set; }
    }
}
