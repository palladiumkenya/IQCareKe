using System;
using System.Xml.Serialization;
using IQCare.Common.BusinessProcess.Commands.Encounter;

using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PatientMasterVisit
{
    public class AddPatientMasterVisitCommand: IRequest<Result<AddPatientMasterVisitResponse>>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public int? VisitScheduled { get; set; }
        public int? VisitBy { get; set; }
        public int? VisitType { get; set; }
        public DateTime? VisitDate { get; set; }
        public bool Active { get; set; }
        public int? Status { get; set; }
        [XmlIgnore]
        public string AuditData { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public bool DeleteFlag { get; set; }
    }

    public class AddPatientMasterVisitResponse
    {
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
    }
}