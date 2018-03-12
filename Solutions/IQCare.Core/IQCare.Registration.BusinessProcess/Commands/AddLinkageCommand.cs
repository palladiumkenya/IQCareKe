using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Registration.BusinessProcess.Commands
{
    using Response = Result<AddLinkageResponse>;

    public class AddLinkageCommand : IRequest<Response>
    {
        public string Facility { get; set; }
        public string HealthWorker { get; set; }
        public string Carde { get; set; }
        public DateTime DateEnrolled { get; set; }
        public string CCCNumber { get; set; }
        public string Remarks { get; set; }
    }

    public class AddLinkageResponse
    {
        public int LinkageId { get; set; }
    }
}
