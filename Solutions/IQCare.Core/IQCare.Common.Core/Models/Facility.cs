namespace IQCare.Common.Core.Models
{
    public class Facility
    {
        public int FacilityID { get; set; }
        public string FacilityName { get; set; }
        public string PosID { get; set; }
        public int? Preferred { get; set; }
        public int DeleteFlag { get; set; }


        public int DosageFrequency { get; set; }
    }
}