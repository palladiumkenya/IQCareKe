using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
    public class PatientProfileCommand : IRequest<Result<ProfileCommandResult>>
    {
        public PatientProfile PatientProfile;
    }

    public class ProfileCommandResult
    {
        public int Id { get; set; }
    }
}