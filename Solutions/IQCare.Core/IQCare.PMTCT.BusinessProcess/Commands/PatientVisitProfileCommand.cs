using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
    public class PatientVisitProfileCommand  : IRequest<Result<PatientVisitDetailsCommandResult>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public decimal? AgeMenarche { get; set; }
        public int? PregnancyId { get; set; }
        public int? VisitNumber { get; set; }
        public int? VisitType { get; set; }
        public int? TreatedForSyphilis { get; set; }
        public Boolean DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
        public string PostPartum { get; set; }
    }
    public class PatientVisitDetailsCommandResult
    {
        public int PatientMasterVisitId { get; set; }
    }
}
