namespace IQCare.Common.Core.Models
{
    public class HtsScreening
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int PatientScreeningId { get; set; }

        public int HtsScreeningOptionsId { get; set; }
    }
}