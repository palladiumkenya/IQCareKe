using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Commands.Maternity
{
    public class AddMaternalPatientDeliveryInfoCommand : IRequest<Result<AddPatientDeliveryInfoResponse>>
    {
        public int PatientMasterVisitId { get; private set; }
        public int ProfileId { get; private set; }
        public int DurationOfLabour { get; private set; }
        public DateTime DateOfDelivery { get; private set; }
        public DateTime TimeOfDelivery { get; private set; }
        public int? ModeOfDelivery { get; private set; }
        public int? PlacentaComplete { get; private set; }
        public int? BloodLossCapacity { get; private set; } 
        public int ? BloodLoss { get; private set; }
        public int? MotherCondition { get; private set; }
        public bool DeliveryComplicationsExperienced { get; private set; }
        public string DeliveryComplicationNotes { get; private set; }
        public string DeliveryConductedBy { get; private set; }
        public int CreatedBy { get; private set; }
    }

    public class AddPatientDeliveryInfoResponse
    {
        public int PatientDeliveryId { get; set; }
        public int PatientMasterVisitId { get; set; }

    }
}
