using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.PreventiveServices
{
    public class AddPatientPreventiveServiceCommand :IRequest<Result<AddPatientPreventiveServiceCommandResult>>
    {
        public PreventiveService preventive;
    }

    public class AddPatientPreventiveServiceCommandResult
    {
        public int PreventiveServiceId { get; set; }
    }
}
