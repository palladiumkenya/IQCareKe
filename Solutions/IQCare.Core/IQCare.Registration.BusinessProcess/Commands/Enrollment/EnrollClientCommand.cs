using System;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Registration.BusinessProcess.Commands.Enrollment
{
    public class EnrollClientCommand : IRequest<Result<EnrollClientResponse>>
    {
        //Enrollment
        public ClientEnrollment ClientEnrollment { get; set; }
    }

    public class ClientEnrollment
    {
        public DateTime DateOfEnrollment { get; set; }
        public int ServiceAreaId { get; set; }
        public string EnrollmentNo { get; set; }
        public int PatientId { get; set; }
        public int PersonId { get; set; }
        public int CreatedBy { get; set; }
    }

    public class EnrollClientResponse
    {
        public int IdentifierId { get; set; }
        public string IdentifierValue { get; set; }
    }
}