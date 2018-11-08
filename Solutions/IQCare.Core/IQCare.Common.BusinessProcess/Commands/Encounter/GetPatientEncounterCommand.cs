using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Encounter
{
    public class GetPatientEncounterCommand: IRequest<Result<List<PatientEncounterView>>>
    {
        public int PatientId { get; set; }
        public int EncounterTypeId { get; set; }
    }
}