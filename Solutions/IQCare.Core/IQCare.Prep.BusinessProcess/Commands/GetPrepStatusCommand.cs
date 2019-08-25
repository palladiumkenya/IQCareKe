using System.Collections.Generic;
using IQCare.Library;
using IQCare.Prep.Core.Models;
using MediatR;

namespace IQCare.Prep.BusinessProcess.Commands
{
    public class GetPrepStatusCommand : IRequest<Result<List<PatientPrEPStatus>>>
    {
        public int PatientId { get; set; }
        public int PatientEncounterId { get; set; }
    }
}