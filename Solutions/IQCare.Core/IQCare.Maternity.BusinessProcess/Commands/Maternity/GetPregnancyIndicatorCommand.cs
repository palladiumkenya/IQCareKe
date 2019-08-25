using IQCare.Library;
using IQCare.Maternity.Core.Domain.Maternity;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Commands.Maternity
{
    public class GetPregnancyIndicatorCommand : IRequest<Result<PregnancyIndicator>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
}