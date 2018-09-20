using System;

namespace IQCare.Common.Core.Models
{
    public class PersonLocation
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int? County { get; set; }
        public int? SubCounty { get; set; }
        public int? Ward { get; set; }
        public string Village { get; set; }
        public string Location { get; set; }
        public string SubLocation { get; set; }
        public string LandMark { get; set; }
        public string NearestHealthCentre { get; set; }
        public bool Active { get; set; }
        public int CreatedBy { get; set; }
        public bool DeleteFlag { get; set; }
        public DateTime CreateDate { get; set; }
    }
}