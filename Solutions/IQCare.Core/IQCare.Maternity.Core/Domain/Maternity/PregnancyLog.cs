using System;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class PregnancyLog
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime LMP { get; set; }
        public DateTime EDD { get; set; }
        public int Outcome { get; set; }
        public DateTime? DateOfOutcome { get; set; }
        public bool? Active { get; set; }
        public int CreatedBy { get; set; }
        public int DeleteFlag { get; set; }
        public DateTime CreateDate { get; set; }
    }
}