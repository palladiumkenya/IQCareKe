using System;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Enrollment
{
    public class AddHivReConfirmatoryTestsCommand : IRequest<Result<HivReConfirmatoryTestsResponse>>
    {
        public int PersonId { get; set; }
        public int TypeOfTest { get; set; }
        public int TestResult { get; set; }
        public DateTime TestResultDate { get; set; }
        public int CreatedBy { get; set; }
    }

    public class HivReConfirmatoryTestsResponse
    {
        public string Message { get; set; }
    }
}