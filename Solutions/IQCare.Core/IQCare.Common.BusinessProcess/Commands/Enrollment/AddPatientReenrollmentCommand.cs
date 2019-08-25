using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Enrollment
{
    public class AddPatientReenrollmentCommand :IRequest<Result<AddPatientReenrollmentResponse>>
    {
        public int PatientId { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public int UserId { get; set; }

        public int ServiceAreaId { get; set; }
    }


   public class AddPatientReenrollmentResponse
    {
        public int Id { get; set; }

        public string Message { get; set; }
    }
}
