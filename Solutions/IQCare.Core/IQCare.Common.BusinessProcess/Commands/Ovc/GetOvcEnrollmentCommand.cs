using System;
using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Ovc
{
    public   class GetOvcEnrollmentCommand : IRequest<Result<OvcEnrollmentForm>>
    {
        public int PersonId { get; set; }
    }
}
