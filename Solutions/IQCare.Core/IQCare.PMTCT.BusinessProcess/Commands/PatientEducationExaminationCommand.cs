using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System.Collections.Generic;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
    public class PatientEducationExaminationCommand:IRequest<Result<PatientEducationExaminationResponse>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int BreastExamDone { get; set; }
        public int? TreatedSyphilis { get; set; }
        public int? TestedForSyphilis { get; set; }
        public int? SyphilisTestUsed { get; set; }
        public int? SyphilisResults { get; set; }
        public int CreatedBy { get; set; }
        public List<PatientEducation> CounsellingTopics;
    }

    public class PatientEducationExaminationResponse
    {
       public int resultId { get; set; }
    }
}
