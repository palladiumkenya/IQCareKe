using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.BusinessProcess.Commands.PreventiveServices
{
    public class GetPatientPrevenetiveServicesCommand:IRequest<Result<List<PreventiveService>>>
    {
        public int PatientId { get; set; }
    }

}
