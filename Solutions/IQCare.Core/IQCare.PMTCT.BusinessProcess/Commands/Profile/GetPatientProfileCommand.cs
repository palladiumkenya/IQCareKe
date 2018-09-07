using IQCare.Library;
using MediatR;
using System.Collections.Generic;
using IQCare.PMTCT.Core.Models;

namespace IQCare.PMTCT.BusinessProcess.Commands.Profile
{
    public class GetPatientProfileCommand :IRequest<Result<List<PatientProfile>>>
    {
        public int PatientId { get; set; }
        public int PregnancyId { get; set; }
    }
}
