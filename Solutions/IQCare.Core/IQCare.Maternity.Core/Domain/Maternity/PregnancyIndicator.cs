using System;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class PregnancyIndicator
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime? LMP { get; set; }
        public DateTime? EDD { get; set; }
        public int PregnancyStatusId { get; set; }
        public int? PregnancyPlanned { get; set; }
        public int? PlanningToGetPregnant { get; set; }
        public int? BreastFeeding { get; set; }
        public bool ANCProfile { get; set; }
        public DateTime? ANCProfileDate { get; set; }
        public bool Active { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
        public DateTime? VisitDate { get; set; }
    }
}
 