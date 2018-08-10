using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.Profile
{
    public class DeletePatientProfileCommand:IRequest<Result<DeletePatientProfileCommandResult>>
    {
        public int ProfileId { get; set; }
        public int PatientId { get; set; }
    }

    public class DeletePatientProfileCommandResult
    {
        public int ProfileId { get; set; }
    }
}
