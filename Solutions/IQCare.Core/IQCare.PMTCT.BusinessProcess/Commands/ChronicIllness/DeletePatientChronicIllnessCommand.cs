using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.ChronicIllness
{
    public class DeletePatientChronicIllnessCommand:IRequest<Result<PatientChronicIllness>>
    {
        public int Id { get; set; }
    }
}