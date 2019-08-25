using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class GetPatientTransferInCommand : IRequest<Result<Core.Models.PatientTransferIn>>
    {
        public int PersonId { get; set; }

        public int ServiceId { get; set; }
    }
}
