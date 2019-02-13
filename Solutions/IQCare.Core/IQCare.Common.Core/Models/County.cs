namespace IQCare.Common.Core.Models
{
    public class County
    {
        public int Id { get; set; }

        public int CountyId { get; set; }

        public string CountyName { get; set; }

        public int SubcountyId { get; set; }

        public string Subcountyname { get; set; }

        public int WardId { get; set; }

        public string WardName { get; set; }
    }
}