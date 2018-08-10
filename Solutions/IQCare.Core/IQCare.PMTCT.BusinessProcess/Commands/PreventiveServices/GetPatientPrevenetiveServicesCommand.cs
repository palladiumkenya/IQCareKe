using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.BusinessProcess.Commands.PreventiveServices
{
    public class GetPatientPrevenetiveServicesCommand:IRequest<Result<GetPatientPreventiveServiceCommandResult>>
    {
        public int PatientId { get; set; }
    }

    public class GetPatientPreventiveServiceCommandResult
    {
        public List<PreventiveService> PreventiveServices;
    }
}
