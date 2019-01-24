using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.BusinessProcess.Commands.Profile
{
 public   class EditPatientProfileCommand : IRequest<Result<EditPatientCommandResult>>
    {
        public PatientProfile PatintProfile;
    }

    public class EditPatientCommandResult
    {
        public int profileId { get; set; }
    }
}
