using IQCare.Library;
using MediatR;
using System;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class AddPatientCommand : IRequest<Result<AddPatientResponse>>
    {
        public int PersonId { get; set; }
        public int UserId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string PosId { get; set; }
        public int PatientType { get; set; }
    }

    public class AddPatientResponse
    {
        public int PatientId { get; set; }
    }
}