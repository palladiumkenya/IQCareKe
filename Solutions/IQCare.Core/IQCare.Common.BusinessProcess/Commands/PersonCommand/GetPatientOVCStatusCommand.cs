using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
   public class GetPatientOVCStatusCommand : IRequest<Result<Core.Models.PatientOVCStatus>>
    {
        public int PersonId { get; set; }
    }
}
