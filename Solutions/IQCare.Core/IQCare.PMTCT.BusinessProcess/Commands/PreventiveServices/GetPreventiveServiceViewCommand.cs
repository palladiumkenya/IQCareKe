using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.Views;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.PreventiveServices
{
    public class GetPreventiveServiceViewCommand :IRequest<Result<List<PatientPreventiveServiceView>>>
    {
        public int PatientId { get; set; }
       // public int PatientMasterVisitId { get; set; }
    }
}