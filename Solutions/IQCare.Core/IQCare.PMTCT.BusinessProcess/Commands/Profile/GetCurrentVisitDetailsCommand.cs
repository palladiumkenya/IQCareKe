using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.Profile
{
   public class GetCurrentVisitDetailsCommand : IRequest<Result<PatientProfile>>
    {
        public int PatientId { get; set; }
    }
}
