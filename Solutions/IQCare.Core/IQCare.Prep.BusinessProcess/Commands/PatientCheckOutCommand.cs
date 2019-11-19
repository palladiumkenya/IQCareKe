using System;
using System.Collections.Generic;
using System.Text;

using IQCare.Library;
using MediatR;
namespace IQCare.Prep.BusinessProcess.Commands
{
    public class PatientCheckOutCommand:IRequest<Result<CheckOutResponse>>
    {
        public int PatientId { get; set; }

        public int Id { get; set; }

        public int ServiceId { get; set; }

        public int UserId { get; set; }

        public int? Status { get; set; }

        public DateTime? VisitDate { get; set; }

        public bool DeleteFlag { get; set; }

    }

    public class CheckOutResponse
    {
        public int Id { get; set; }

        public string Message { get; set; }
    }
}
