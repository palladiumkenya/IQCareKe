using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Commands.Maternity
{
    public class DischargePatientCommand : IRequest<Result<DischargePatientResponse>>
    {
        public int? PatientMasterVisitId { get;  set; }
        public int? OutcomeStatus { get;  set; }
        public string OutcomeDescription { get;  set; }
        public DateTime ? DateDischarged { get; set; }
        public int CreatedBy { get; set; }
    }

    public class DischargePatientResponse
    {
        public int PatientDischargeId { get; set; }
        public string Message { get; set; }
        
    }

    public class UpdatePatientDischargeCommand : IRequest<Result<DischargePatientResponse>>
    {
        public int Id { get; set; }
        public int? OutcomeStatus { get; set; }
        public string OutcomeDescription { get; set; }
        public DateTime? DateDischarged { get; set; }

    }

}
