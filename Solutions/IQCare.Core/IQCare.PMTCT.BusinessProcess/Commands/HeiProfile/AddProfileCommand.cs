using System;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiProfile
{
    public class AddProfileCommand:IRequest<Result<PatientHeiProfile>>
    {
        public PatientHeiProfile PatientHeiProfile;
    }
}
