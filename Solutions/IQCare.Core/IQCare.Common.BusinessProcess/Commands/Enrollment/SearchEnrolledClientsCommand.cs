using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Enrollment
{
    public class SearchEnrolledClientsCommand : IRequest<Result<List<PatientLookupView>>>
    {
        public string identificationNumber { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
    }

    public class SearchEnrolledClientsResponse
    {
        public List<PatientLookupView> PatientSearch { get; set; }
    }
}