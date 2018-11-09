using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.HTS.Core.Model;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class GetPersonLinkageCommand : IRequest<Result<List<PatientLinkage>>>
    {
        public int PersonId { get; set; }
    }
}