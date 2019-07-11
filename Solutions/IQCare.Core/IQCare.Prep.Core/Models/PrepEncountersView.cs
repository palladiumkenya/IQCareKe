using System;

namespace IQCare.Prep.Core.Models
{
    public class PrepEncountersView
    {
        public Int64 RowID { get; set; }
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public int ServiceAreaId { get; set; }
        public int EncounterTypeId { get; set; }

        public string EncounterType { get; set; }
        public int? PrepStatusToday { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? EncounterStartTime { get; set; }
        public string PreStatus { get; set; }
        public string ProviderName { get; set; }
    }
}