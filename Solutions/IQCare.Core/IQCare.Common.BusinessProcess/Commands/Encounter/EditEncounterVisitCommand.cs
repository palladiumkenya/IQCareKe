using System;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Encounter
{
    public class EditEncounterVisitCommand : IRequest<Result<EditEncounterVisitCommandResponse>>
    {
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime EncounterDate { get; set; }
    }

    public class EditEncounterVisitCommandResponse
    {
        public string Message { get; set; }
    }
}