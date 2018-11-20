using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Queries.Maternity
{
    public class GetPatientDeliveryInformationQuery : IRequest<Result<List<PatientDeliveryInfomationViewModel>>>
    {
        public int ProfileId { get; set; }

    }

    public class PatientDeliveryInfomationViewModel
    {
        public int Id { get;  set; }
        public int PatientMasterVisitId { get;  set; }
        public int ProfileId { get;  set; }
        public string DurationOfLabour { get;  set; }
        public DateTime DateOfDelivery { get;  set; }
        public TimeSpan TimeOfDelivery { get;  set; }
        public string ModeOfDelivery { get;  set; }
        public string PlacentaComplete { get;  set; }
        public int? BloodLossCapacity { get;  set; }
        public string BloodLossClassification { get; set; }
        public string MotherCondition { get;  set; }
        public string DeliveryComplicationsExperienced { get;  set; }
        public string DeliveryComplicationNotes { get;  set; }
        public string DeliveryConductedBy { get;  set; }
        public string MaternalDeathAudited { get;  set; }
        public DateTime? MaternalDeathAuditDate { get;  set; }
        public string ApgarScores { get; set; }
    }
}
