using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Enrollment
{
    public class GetPatientCareEndedByServiceAreaCommand : IRequest<Result<List<PatientCareEndingServiceArea>>>
    {
        public int PersonId { get; set; }
    }
}

 
