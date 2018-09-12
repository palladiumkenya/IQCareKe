using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System.Collections.Generic;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
    public class HaartProphylaxisCommand :IRequest<Result<HaartProphylaxisResponse>>
    {
        public List<PatientDrugAdministration> PatientDrugAdministration;
        public List<PatientChronicIllness> PatientChronicIllnesses;
        public int OtherIllness { get; set; }
    }

    public class HaartProphylaxisResponse
    {
        public int PreventivePartnerId { get; set; }
    }
}
