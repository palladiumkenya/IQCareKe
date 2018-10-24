using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;

namespace IQCare.Maternity.BusinessProcess.Commands
{
    public class PatientMasterVisitCommand : IRequest<Result<AddPatientMasterVisitResponse>>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Active { get; set; }
        public DateTime VisitDate { get; set; }
        public int VisitScheduled { get; set; }
        public int VisitBy { get; set; }
        public int VisitType { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public bool DeleteFlag { get; set; }
        public string CreatedBy { get; set; }
        public string AuditData { get; set; }

    }

    public class AddPatientMasterVisitResponse
    {
        public int Id { get; set; }

    }
}
