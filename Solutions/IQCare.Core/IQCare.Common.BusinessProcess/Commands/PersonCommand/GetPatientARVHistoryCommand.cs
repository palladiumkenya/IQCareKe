using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
   public class GetPatientARVHistoryCommand: IRequest<Result<PatientARVHistory>>
    {
        public int PersonId { get; set; }

        public int ServiceId { get; set; }
    }
}
