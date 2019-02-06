using System;
using System.Collections.Generic;
using IQCare.Library;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Commands.PNC
{
    public class UpdatePatientExaminationCommand : IRequest<Result<PatientExaminationResponse>>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ExaminationTypeId { get; set; }
        public int CreateBy { get; set; }
        public Boolean DeleteFlag { get; set; }
        public List<PostNatalExamResult> PostNatalExamResults { get; set; }
    }

    public class PatientExaminationResponse
    {
        public string Message { get; set; }
    }
}