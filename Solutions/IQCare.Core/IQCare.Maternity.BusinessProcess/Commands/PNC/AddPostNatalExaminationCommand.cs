using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Commands.PNC
{
    public class AddPostNatalExaminationCommand: IRequest<Result<PostnatalExamResultsResponse>>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ExaminationTypeId { get; set; }
        public int CreateBy { get; set; }
        public Boolean DeleteFlag { get; set; }
        public List<PostNatalExamResult> PostNatalExamResults { get; set; }
    }

    public class PostNatalExamResult
    {
        public int ExamId { get; set; }
        public int FindingId { get; set; }
        public string FindingsNotes { get; set; }
    }
    public class PostnatalExamResultsResponse
    {
        public int PatientId { get; set; }
    }
}
