using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System.Collections.Generic;

namespace IQCare.Common.BusinessProcess.Commands.Encounter
{
    public class GetPatientEncounterCommand: IRequest<Result<List<PatientEncounterView>>>
    {
        public int PatientId { get; set; }
        public int EncounterTypeId { get; set; }
    }
}