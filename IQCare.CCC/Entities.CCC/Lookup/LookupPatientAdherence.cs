using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("AdherenceOutcome")]
    public class LookupPatientAdherence
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int AdherenceType { get; set; }
        public int Score { get; set; }
    }
}
