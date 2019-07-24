using System.Collections.Generic;
using IQCare.Library;
using IQCare.Maternity.Core.Domain.Maternity;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Commands.Maternity
{
    public class GetPregnancyLogCommand : IRequest<Result<List<PregnancyLog>>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
}