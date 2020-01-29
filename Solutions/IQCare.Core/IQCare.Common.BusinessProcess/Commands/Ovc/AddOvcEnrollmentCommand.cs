using System;
using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;


namespace IQCare.Common.BusinessProcess.Commands.Ovc
{
    public class AddOvcEnrollmentCommand : IRequest<Result<Response>>
    {
        public int PersonId { get; set; }

        public string PartnerOVCServices { get; set; }

        public int CPMISEnrolled { get; set; }

        public DateTime? EnrollmentDate { get; set; } 

        public int CreatedBy { get; set; }

       
    }

    public class Response
    {
        public string Message { get; set; }
    }
}
