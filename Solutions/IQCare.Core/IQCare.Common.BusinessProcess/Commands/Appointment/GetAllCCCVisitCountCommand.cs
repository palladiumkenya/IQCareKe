using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.Appointment
{
    public class GetAllCCCVisitCountCommand : IRequest<Result<CCCVisitCountResponse>>
    {
        public DateTime SummaryDate { get; set; }
    }

    public class CCCVisitCountResponse
    {
        public int TotalVisits { get; set; }
    }
}
