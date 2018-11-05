using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Commands.PNC
{
    public class PatientPartnerTestingCommand : IRequest<Result<PatientPatnerTestingResponse>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PartnerTested { get; set; }
        public int PartnerHIVResult { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public Boolean DeleteFlag { get; set; }
        public string AuditData { get; set; }
    }
    public class PatientPatnerTestingResponse
    { public int PatientId { get; set; } }
}
