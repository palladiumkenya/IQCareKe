using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System.Collections.Generic;

namespace IQCare.PMTCT.BusinessProcess.Commands.Education
{
    public class GetPatientEducationCommand:IRequest<Result<GetPatientEducationCommandResult>>
    {
        public int PatientId { get; set; }
    }

    public class GetPatientEducationCommandResult
    {
        public List<PatientEducation> patientEducations;
    }
}
