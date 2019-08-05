using System.Collections.Generic;
using IQCare.Library;
using MediatR;

namespace IQCare.Lab.BusinessProcess.Commands
{
    public class AddHeiLabTestsDoneCommand : IRequest<Result<AddHeiLabTestsDoneResponse>>
    {
        public int LabOrderId { get; set; }
        public int PatientId { get; set; }
        public List<TestType> HeiLabTestTypes { get; set; }
    }

    public class TestType
    {
        public int Id { get; set; }
    }

    public class AddHeiLabTestsDoneResponse
    {
        public string Message { get; set; }
    }
}