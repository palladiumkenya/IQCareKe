using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System;

namespace IQCare.PMTCT.BusinessProcess.Commands.Education
{
    public class AddPatientEducationCommand:IRequest<Result<AddPatientEducationCommandResult>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int CounsellingTopicId { get; set; }
        public bool  IsCounsellingDone { get; set; }
        public DateTime ? CounsellingDate { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
    }

    public class AddPatientEducationCommandResult
    {
        public int PatientCounsellingId { get; set; }
    }

}
