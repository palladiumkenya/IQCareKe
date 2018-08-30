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
        public int BreastExam { get; set; }
        public int Syphillis { get; set; }
        public List<PatientEducation> patientEducation;
    }

    public class PatientEducationExaminationResponse
    {
       public int resultId { get; set; }
    }
}
