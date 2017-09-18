using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.HTS
{
    [Serializable]
    public class Encounter
    {
        [Key]
        public int Id { get; set; }
        public string SysUuid { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public Provider Provider { get; set; }
        public int ProviderId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompleteDate { get; set; }
        public string GeoLocation { get; set; }
        public int EverTested { get; set; }
        public int MonthSinceLastTest { get; set; }
        public int Disability { get; set; }
        public int ConsentToTest { get; set; }
        public int TestedAs { get; set; }
        public int TestStrategyId { get; set; }
        public int TestCategoryId { get; set; }
        public int CompleteStatus { get; set; }
        public string EncounterIdentifier { get; set; }

    }
}
