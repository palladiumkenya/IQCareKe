using System;
using IQCare.Library;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Commands.Maternity
{
    public class AddPregnancyLogCommand : IRequest<Result<AddPregnancyLogResponse>>
    {
        public int? Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime LMP { get; set; }
        public DateTime EDD { get; set; }
        public int Outcome { get; set; }
        public DateTime? DateOfOutcome { get; set; }
        public int CreatedBy { get; set; }
        public int? BirthDefects { get; set; }
    }

    public class AddPregnancyLogResponse
    {
        public string Message { get; set; }
    }
}