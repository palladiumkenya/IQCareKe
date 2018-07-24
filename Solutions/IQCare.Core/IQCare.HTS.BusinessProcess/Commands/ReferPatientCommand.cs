using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class ReferPatientCommand : IRequest<Result<ReferPatientResponse>>
    {
        public int ReferredTo { get; set; }
        public DateTime DateToBeEnrolled { get; set; }
        public int ReferralReason { get; set; }
        public int UserId { get; set; }
        public int ServiceAreaId { get; set; }
        public int PersonId { get; set; }
        public int FromFacilityId { get; set; }
        public List<TracingArray> Tracing { get; set; }
        public bool IsEdit { get; set; }
    }

    public class TracingArray
    {
        public DateTime TracingDate { get; set; }
        public int Mode { get; set; }
        public int Outcome { get; set; }
        public int TracingType { get; set; }
    }

    public class ReferPatientResponse
    {
        public int ReferralId { get; set; }
    }
}
