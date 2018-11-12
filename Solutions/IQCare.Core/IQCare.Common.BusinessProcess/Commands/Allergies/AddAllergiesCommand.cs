using IQCare.Common.Core.Models;
using MediatR;
using System;

namespace IQCare.Common.BusinessProcess.Commands.Allergies
{
    public class AddAllergiesCommand : IRequest<Result<AddPatientAllergiesResponse>>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public string Allergen { get; set; }
        public Boolean DeleteFlag { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
        public int Reaction { get; set; }
        public int Severity { get; set; }
        public DateTime OnsetDate { get; set; }

    }

    public class AddPatientAllergiesResponse
    {
        public int PatientId { get; set; }

    }
}
