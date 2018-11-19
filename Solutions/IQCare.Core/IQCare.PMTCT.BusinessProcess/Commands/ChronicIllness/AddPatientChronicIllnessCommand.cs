using System;
using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.ChronicIllness
{
    public class AddPatientChronicIllnessCommand : IRequest<Result<PatientChronicIllness>>
    {
        public List<PatientChronicIllness> PatientChronicIllnesses { get; set; }
    }

}