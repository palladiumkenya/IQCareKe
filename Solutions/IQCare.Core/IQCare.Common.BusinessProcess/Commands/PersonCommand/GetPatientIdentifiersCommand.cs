using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class GetPatientIdentifiersCommand : IRequest<Result<List<PatientIdentifier>>>
    {
        public int PatientId { get; set; }
    }
}