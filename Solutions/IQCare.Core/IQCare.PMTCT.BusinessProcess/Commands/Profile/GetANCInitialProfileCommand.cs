using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.BusinessProcess.Commands.Profile
{
  public  class GetPatientInitialProfileCommand :IRequest<Result<PatientProfile>>
    {
        public int PatientId { get; set; }
        public int ? PregnancyId { get; set; }
    }
}
