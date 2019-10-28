using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;
using IQCare.Prep.Core.Models;

namespace IQCare.Prep.BusinessProcess.Commands
{
    public   class GetPatientAdherenceOutcomeCommand :IRequest<Result<List<AdherenceView>>>
    {
        public int PatientId { get; set; }
    }
}
