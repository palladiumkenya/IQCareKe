using System;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class AddLinkageCommand : IRequest<Result<AddLinkageResponse>>
    {
        public int PersonId { get; set; }
        public string FacilityId { get; set; }
        public string HealthWorker { get; set; }
        public string Carde { get; set; }
        public DateTime DateEnrolled { get; set; }
        public string CCCNumber { get; set; }
        public string Remarks { get; set; }
        public int UserId { get; set; }
    }

    public class AddLinkageResponse
    {
        public int LinkageId { get; set; }
    }
}