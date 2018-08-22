using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System.Collections.Generic;

namespace IQCare.PMTCT.BusinessProcess.Commands.PreventiveServices
{
    public class PatientPreventiveServiceCommand :IRequest<Result<AddPatientPreventiveServiceCommandResponse>>
    {
        public List<PreventiveService>  preventiveService;
    }

    public class AddPatientPreventiveServiceCommandResponse
    {
        public int PreventiveServiceId { get; set; }
    }
}
