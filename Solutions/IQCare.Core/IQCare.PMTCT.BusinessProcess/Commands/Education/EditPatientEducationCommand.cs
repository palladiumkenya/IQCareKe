using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.Education
{
    public class EditPatientEducationCommand:IRequest<Result<EditPatientEducationCommadResult>>
    {
        public PatientEducation patientEducation;
    }

    public class EditPatientEducationCommadResult
    {
        public int PatientEducationId { get; set; }
    }
}
