using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System.Collections.Generic;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiDeliveryMaternalHistory
{
    public class GetMaternalViralLoadCommand : IRequest<Result<GetMaternalViralLoadResult>>
    {
        public int PatientId { get; set; }
    }

    public class GetMaternalViralLoadResult
    {
        public List<PatientViralLoadPoco> PatientViralLoad { get; set; }
    }
}