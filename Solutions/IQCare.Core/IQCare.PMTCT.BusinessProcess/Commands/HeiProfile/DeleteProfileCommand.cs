using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiProfile
{
   public class DeleteProfileCommand:IRequest<Result<PatientHeiProfile>>
    {
        public int PatientId { get; set; }
    }
}
