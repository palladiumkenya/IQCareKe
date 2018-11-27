using System;
using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.VisitDetails
{
    public class AddVisitDetailsCommand: IRequest<Result<Core.Models.VisitDetails>>
    {
        public int PatientId { get; set; }
        public int ServiceAreaId { get; set; }
        public int? PregnancyId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public int? VisitNumber { get; set; }
        public int? DaysPostPartum { get; set; }
        public int? VisitType { get; set; }
        public int UserId { get; set; }
    }
}