using System;
using IQCare.Library;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Commands.Maternity
{
    public class AddPregnancyIndicatorCommand : IRequest<Result<PregnancyIndicatorResult>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime? LMP { get; set; }
        public DateTime? EDD { get; set; }
        public int PregnancyStatusId { get; set; }
        public bool ANCProfile { get; set; }
        public DateTime? ANCProfileDate { get; set; }
        public bool Active { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
        public DateTime VisitDate { get; set; }
    }

    public class PregnancyIndicatorResult
    {
        public string Message { get; set; }
    }
}