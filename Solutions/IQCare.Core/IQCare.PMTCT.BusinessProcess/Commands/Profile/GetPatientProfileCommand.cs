using IQCare.Library;
using MediatR;
using System.Collections.Generic;

namespace IQCare.PMTCT.BusinessProcess.Commands.Profile
{
    public class GetPatientProfileCommand :IRequest<Result<GetPatientProfileCommandResult>>
    {
        public int PatientId { get; set; }
    }

    public class GetPatientProfileCommandResult {

        public List<PatientProfileCommand>  patientProfile;
     }
}
