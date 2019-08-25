using System;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class AddLinkageCommand : IRequest<Result<AddLinkageResponse>>
    {
        public int PersonId { get; set; }
        public string Facility { get; set; }
        public string HealthWorker { get; set; }
        public string Carde { get; set; }
        public DateTime DateEnrolled { get; set; }
        public string CCCNumber { get; set; }
        public string Remarks { get; set; }
        public int UserId { get; set; }
        public bool IsEdit { get; set; } = false;
        public int? Id { get; set; }
        public DateTime? Artstartdate { get; set; }
    }

    public class AddLinkageResponse
    {
        public int LinkageId { get; set; }
    }
}