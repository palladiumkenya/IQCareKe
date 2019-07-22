using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.BusinessProcess.Commands
{
    public class RiskAssessmentVisitDetailCommand:IRequest<Result<RiskAssessmentResponse>>
    {

        public int EncounterTypeId { get; set; }
        public int UserId { get; set; }
        public int PatientId { get; set; }

        public int PatientMasterVisitId { get; set; }
        public DateTime VisitDate { get; set; }

        public int ServiceAreaId { get; set; }

        public List<RiskAssessmentList> riskAssessments { get; set; }

        public List<ClinicalNotes> ClinicalNotes { get; set; }

        
    }

    public class RiskAssessmentList
    {
        public int Id { get; set; }

        public string Comment { get; set;}

        public int RiskAssessmentid { get; set; }

        public int Value { get; set; }

        public bool DeleteFlag { get; set; }

        public DateTime? Date { get; set; }


    }


    public class ClinicalNotes
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public int ServiceAreaId { get; set; }

        public bool DeleteFlag { get; set; }

        
    }

    public class RiskAssessmentResponse
    {
        public int  PatientMasterVisitId { get; set; }

        public string Message { get; set; }
    }
   
}
