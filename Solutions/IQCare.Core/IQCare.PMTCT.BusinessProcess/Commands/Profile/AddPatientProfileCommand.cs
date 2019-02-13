using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.Profile
{
   public class AddPatientProfileCommand:IRequest<Result<PatientProfileCommandResult>>
    {
        public PatientProfileCommand patientProfile;
    }

    public class PatientProfileCommandResult
    {
        public int profileId { get; set; }
    }
}
