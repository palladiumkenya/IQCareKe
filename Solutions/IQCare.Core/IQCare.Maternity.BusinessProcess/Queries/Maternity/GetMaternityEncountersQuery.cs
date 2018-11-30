using System.Collections.Generic;
using IQCare.Library;
using IQCare.Maternity.Core.Domain.Maternity;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Queries.Maternity
{
    public class GetMaternityEncountersQuery:IRequest<Result<List<MaternityEncounter>>>
    {
        public int PatientId { get; set; }
    }
}