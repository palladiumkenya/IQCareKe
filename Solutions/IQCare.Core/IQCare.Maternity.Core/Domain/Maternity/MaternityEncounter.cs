using System;
using System.ComponentModel.DataAnnotations;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class MaternityEncounter
    {
        [Key]
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int EncounterTypeId { get; set; }
        public DateTime EncounterStartTime { get; set; }
        public DateTime EncounterEndTime { get; set; }
        public int PatientId { get; set; }
        public int VisitNumber { get; set; }
    }
}