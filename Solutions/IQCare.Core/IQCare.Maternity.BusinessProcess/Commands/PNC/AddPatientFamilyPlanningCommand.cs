using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Commands.PNC
{
    public class AddPatientFamilyPlanningCommand : IRequest<Result<AddFamilyPlaaningResultsResponse>>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int FamilyPlanningStatusId { get; set; }
        public int ReasonNotOnFPId { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime VisitDate { get; set; }
        public string AuditData { get; set; }
    }
    public class AddFamilyPlaaningResultsResponse
    {
        public int PatientId { get; set; }
    }

    public class GetPatientFamilyPlanningQuery : IRequest<Result<List<PatientFamilyPlanningViewModel>>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
    public class PatientFamilyPlanningViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int FamilyPlanningStatusId { get; set; }
        public int ReasonNotOnFPId { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime VisitDate { get; set; }
        public string AuditData { get; set; }

    }

}
