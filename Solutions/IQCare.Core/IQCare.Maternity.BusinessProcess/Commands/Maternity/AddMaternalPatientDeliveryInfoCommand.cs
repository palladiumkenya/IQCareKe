using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Commands.Maternity
{
    public class AddMaternalPatientDeliveryInfoCommand : IRequest<Result<AddPatientDeliveryInfoResponse>>
    {
        public int PatientMasterVisitId { get;  set; }
        public int ProfileId { get;  set; }
        public int DurationOfLabour { get;  set; }
        public DateTime DateOfDelivery { get;  set; }
        public DateTime TimeOfDelivery { get;  set; }
        public int? ModeOfDelivery { get;  set; }
        public int? PlacentaComplete { get;  set; }
        public int? BloodLossCapacity { get;  set; } 
        public int ? BloodLossClassification { get;  set; }
        public int? MotherCondition { get;  set; }
        public int ? MaternalDeathAudited { get;  set; }
        public DateTime? MaternalDeathAuditDate { get;  set; }
        public bool DeliveryComplicationsExperienced { get;  set; }
        public string DeliveryComplicationNotes { get;  set; }
        public string DeliveryConductedBy { get;  set; }
        public int CreatedBy { get;  set; }
    }

    public class AddPatientDeliveryInfoResponse
    {
        public int PatientDeliveryId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ProfileId { get; set; }
        
    }
}
