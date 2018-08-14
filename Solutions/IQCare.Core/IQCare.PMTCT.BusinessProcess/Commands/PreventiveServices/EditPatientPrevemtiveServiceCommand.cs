using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.PreventiveServices
{
    public class EditPatientPrevemtiveServiceCommand :IRequest<Result<EditPreventiveServiceCommandResult>>
    {
        public PreventiveService preventiveService;
    }

    public class EditPreventiveServiceCommandResult
    {
        public int PreventiServiceId { get; set; }
    }

}
