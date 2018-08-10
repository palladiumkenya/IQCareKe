using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System.Collections.Generic;

namespace IQCare.PMTCT.BusinessProcess.Commands.Education
{
    public class AddPatientEducationCommand:IRequest<Result<AddPatientEducationCommandResult>>
    {
        public PatientEducation patientEducation;
    }

    public class AddPatientEducationCommandResult
    {
        public List<PatientEducation> PatientCounsellingId { get; set; }
    }

}
