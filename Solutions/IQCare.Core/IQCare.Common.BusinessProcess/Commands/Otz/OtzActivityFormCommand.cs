using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.Otz
{
    public class OtzActivityFormCommand : IRequest<Result<OtzActivityFormResponse>>
    {
        public List<OtzActivity> OtzActivity { get; set; }
        public int AttendedSupportGroup { get; set; }
        public string Remarks { get; set; }
        public DateTime VisitDate { get; set; }
        public int UserId { get; set; }
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
    }

    public class OtzActivity
    {
        public int TopicId { get; set; }
        public DateTime DateCompleted { get; set; }
    }

    public class OtzActivityFormResponse
    {
        public string Message { get; set; }
    }
}
