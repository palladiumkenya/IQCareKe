using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;

namespace IQCare.Common.BusinessProcess.Commands.Allergies
{
    public class AddAllergiesCommand : IRequest<Result<AddPatientAllergiesResponse>>
    {
        public List<PatientAllergy> PatientAllergies { get; set; }
    }

    public class AddPatientAllergiesResponse
    {
        public string Message { get; set; }
    }

    public class GetPatientAllergies : IRequest<Result<List<PatientAllergiesViewModel>>>
    {
        public int PatientId { get; set; }
    }
    public class PatientAllergiesViewModel
    
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int? PatientMasterVisitId { get; set; }
        public string Allergen { get; set; }
        public string AllergenName { get; set; }
        public Boolean DeleteFlag { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        //public string AuditData { get; set; }
        public int Reaction { get; set; }
        public string ReactionName { get; set; }
        public int Severity { get; set; }
        public string SeverityName { get; set; }
        public DateTime? OnsetDate { get; set; }
    }
}
