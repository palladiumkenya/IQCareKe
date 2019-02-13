using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.Education
{
    public class EditPatientEducationCommand:IRequest<Result<EditPatientEducationCommadResult>>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CounsellingTopicId { get; set; }
    }

    public class EditPatientEducationCommadResult
    {
        public int PatientEducationId { get; set; }
    }
}
