using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Queries.Maternity
{
    public class GetPatientDeliveryInformationQuery : IRequest<List<PatientDeliveryInfomationViewModel>>
    {
        public int ProfileId { get; set; }

    }

    public class PatientDeliveryInfomationViewModel
    {
        public int Id { get;  set; }
        public int PatientMasterVisitId { get;  set; }
        public int ProfileId { get;  set; }
        public int DurationOfLabour { get;  set; }
        public DateTime DateOfDelivery { get;  set; }
        public DateTime TimeOfDelivery { get;  set; }
        public string ModeOfDelivery { get;  set; }
        public string PlacentaComplete { get;  set; }
        public int? BloodLossCapacity { get;  set; }
        public string BloodLossClassification { get; set; }
        public string MotherCondition { get;  set; }
        public string DeliveryComplicationsExperienced { get;  set; }
        public string DeliveryComplicationNotes { get;  set; }
        public string DeliveryConductedBy { get;  set; }
        public int? MaternalDeathAudited { get;  set; }
        public DateTime? MaternalDeathAuditDate { get;  set; }
    }
}
