using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.Encounter
{
    public class GetPatientEncountersCompletedCommand : IRequest<Result<List<PatientEncountersCompletedPoco>>>
    {
        public int PatientId { get; set; }
    }
}
