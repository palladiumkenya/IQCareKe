using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.Views;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
    public class GetPhysicalExaminationViewCommand: IRequest<Result<List<PhysicalExaminationView>>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
}