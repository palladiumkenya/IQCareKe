using Common.Core.Model;

namespace TriageManagement.Core.Model
{
    [System.Data.Linq.Mapping.Table(Name = "PatientChronicIllness")]

    public class PatientChronicIllness :BaseEntity
    {
       public int PatientMasterVisitId { get; set; }
        public int ChronicIllness { get; set; }
        public string Treatment { get; set; }
        public int Dose { get; set; }
        public int Duration { get; set; }
    }
}
