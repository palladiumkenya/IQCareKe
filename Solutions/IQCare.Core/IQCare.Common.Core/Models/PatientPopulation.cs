using System;

namespace IQCare.Common.Core.Models
{
    public class PatientPopulation
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string PopulationType { get; set; }
        public int PopulationCategory { get; set; }
        public bool Active { get; set; }
        public int PopulationTypeId { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
    }
}