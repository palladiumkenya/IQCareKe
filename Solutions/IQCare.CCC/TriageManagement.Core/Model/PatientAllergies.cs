using System;
using Common.Core.Model;

namespace TriageManagement.Core.Model
{
    [System.Data.Linq.Mapping.Table(Name = "PatientAllergies")]
    public class PatientAllergies :BaseEntity
    {
        public int PatientMasterVisitId { get; set; }
        public int AllergyType { get; set; }
        public string Allagen { get; set; }
        public string Description { get; set; }
        public DateTime OnsetDate { get; set; }
    }
}
