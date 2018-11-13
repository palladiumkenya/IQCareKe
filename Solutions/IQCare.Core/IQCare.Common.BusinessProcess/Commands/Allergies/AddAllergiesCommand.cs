using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;

namespace IQCare.Common.BusinessProcess.Commands.Allergies
{
    public class AddAllergiesCommand : IRequest<Result<AddPatientAllergiesResponse>>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int AllergyType { get; set; }
        public string Allagen { get; set; }
        public string Description { get; set; }
        public DateTime? OnsetDate { get; set; }
        public int Void { get; set; }
        public int? VoidBy { get; set; }
        public DateTime? VoidDate { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }

    }

    public class AddPatientAllergiesResponse
    {
        public int PatientId { get; set; }

    }

    public class GetPatientAllergies : IRequest<Result<List<PatientAllergiesViewModel>>>
    {
        public int PatientId { get; set; }
    }
    public class PatientAllergiesViewModel
    
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int AllergyType { get; set; }
        public string Allagen { get; set; }
        public string Description { get; set; }
        public DateTime? OnsetDate { get; set; }
        public int Void { get; set; }
        public int? VoidBy { get; set; }
        public DateTime? VoidDate { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
