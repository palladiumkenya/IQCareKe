using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.Views;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
    public class GetPatientScreeningViewCommand: IRequest<Result<List<PmtctPatientScreeningView>>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
}