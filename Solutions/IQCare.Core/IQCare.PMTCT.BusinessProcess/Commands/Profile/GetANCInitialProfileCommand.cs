using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.BusinessProcess.Commands.Profile
{
  public  class GetANCInitialProfileCommand :IRequest<Result<PatientProfile>>
    {
        public int PatientId { get; set; }
    }
}
