using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.ChronicIllness
{
    public class EditPatientChronicIllnessCommand: IRequest<Result<PatientChronicIllness>>
    {
        public PatientChronicIllness PatientChronicIllness { get; set; }
    }
}