using System;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class AddPatientCommand : IRequest<Result<AddPatientResponse>>
    {
        public int PersonId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class AddPatientResponse
    {
        public int PatientId { get; set; }
    }
}