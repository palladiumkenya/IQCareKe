using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.Education
{
    public class AddPatientEducationCommand:IRequest<Result<AddPatientEducationCommandResult>>
    {
        public PatientEducation patientEducation;
    }

    public class AddPatientEducationCommandResult
    {
        public int PatientCounsellingId { get; set; }
    }

}
