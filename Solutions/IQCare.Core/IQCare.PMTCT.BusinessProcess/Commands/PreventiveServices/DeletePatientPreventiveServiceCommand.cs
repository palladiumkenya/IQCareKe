using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.BusinessProcess.Commands.PreventiveServices
{
    public class DeletePatientPreventiveServiceCommand:IRequest<Result<DeletePreventiveServiceCommandResult>>
    {
        public int Id { get; set; }
 
    }
    public class DeletePreventiveServiceCommandResult
    {
        public int PreventiveServiceId { get; set; }
    }
}
