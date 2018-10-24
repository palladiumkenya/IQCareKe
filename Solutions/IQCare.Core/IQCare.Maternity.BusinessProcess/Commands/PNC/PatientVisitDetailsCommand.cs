using IQCare.Library;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Commands.PNCCommands
{
    public class PatientVisitDetailsCommand : IRequest<Result<VisitDetailsCommandResult>>
    {
      
        public int Id { get; set; }
        public string PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Boolean Active { get; set; }
        public DateTime VisitDate { get; set; }
        public int VisitScheduled { get; set; }
        public int VisitBy { get; set; }  
        public int VisitType { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public Boolean DeleteFlag { get; set; }
        public string CreatedBy { get; set; }
        public string AuditData { get; set; }
        public int EncounterTypeId { get; set; }
        public string EncounterStartTime { get; set; }
        public string EncounterEndTime { get; set; }
        public string ServiceAreaId { get; set; }

    }
    public class VisitDetailsCommandResult
    {
        public int PatientMasterVisitId { get; set; }     
        public int EncounterTypeId { get; set; }
    }
}
