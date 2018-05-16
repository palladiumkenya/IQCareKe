namespace IQCare.Common.Core.Models
{
    public class InteropPlacerValue
    {
        public int Id { get; set; }
        public int InteropPlacerTypeId { get; set; }
        public int IdentifierType { get; set; }
        public int EntityId { get; set; }
        public string PlacerValue { get; set; }
    }
}