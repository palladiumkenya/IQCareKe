using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class GetEnrolledServicesCommand : IRequest<Library.Result<EnrolledServicesResponse>>
    {
        public int PersonId { get; set; }
    }

    public class EnrolledServicesResponse
    {
        public List<PatientEnrollment> PersonEnrollmentList { get; set; }
        public List<PatientIdentifier> PatientIdentifiers { get; set; }
        public List<Identifier> Identifiers { get; set; }
    }
}