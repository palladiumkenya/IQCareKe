using System;
using System.Collections.Generic;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Enrollment
{
    public class EnrollClientCommand : IRequest<Core.Models.Result<EnrollClientResponse>>
    {
        public ClientEnrollment ClientEnrollment { get; set; }
    }

    public class ClientEnrollment
    {
        public DateTime DateOfEnrollment { get; set; }
        public List<ServiceIdentifiersList> ServiceIdentifiersList { get; set; }
        public int ServiceAreaId { get; set; }
        //public string EnrollmentNo { get; set; }
        public int PatientId { get; set; }
        public int PersonId { get; set; }
        public int CreatedBy { get; set; }
        public string PosId { get; set; }
    }

    public class EnrollClientResponse
    {
        public string Message { get; set; }
        //public int IdentifierId { get; set; }
        //public string IdentifierValue { get; set; }
    }
}