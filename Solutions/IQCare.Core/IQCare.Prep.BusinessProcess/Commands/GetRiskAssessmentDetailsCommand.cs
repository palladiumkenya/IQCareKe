using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.BusinessProcess.Commands
{
   public class GetRiskAssessmentDetailsCommand : IRequest<Result<GetRiskAssessmentResponse>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }

    public class RiskAssessmentDetails
    {
        public int Id { get; set; }


        public string Comment { get; set; }

        public int RiskAssessmentid { get; set; }

        public int Value { get; set; }

        public bool DeleteFlag { get; set; }

    }

    public class Notes
    {
        public int Id { get; set; }
        public string Comment { get; set; }

        public bool DeleteFlag { get; set; }
    }

    public class GetRiskAssessmentResponse
    {


        public DateTime? VisitDate { get; set; }
        public List<RiskAssessmentDetails> RiskAssessmentDetails { get; set; }

        public List<Notes> ClinicalNotes { get; set; }

    }
}
