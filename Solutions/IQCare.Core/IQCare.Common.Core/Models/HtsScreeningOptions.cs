using System;

namespace IQCare.Common.Core.Models
{
    public class HtsScreeningOptions
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public string Occupation { get; set; }

        public DateTime ScreeningDate { get; set; }

        public DateTime BookingDate { get; set; }
    }
}