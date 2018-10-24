using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class PatientDeliveryInformation
    {
        public PatientDeliveryInformation()
        {

        }
        public PatientDeliveryInformation(int patientMasterVisitId, int profileId, int labourDuration, DateTime deliveryDate, DateTime deliveryTime, int? deliveryMode, int? placentaComplete, int? bloodLoss, int? motherCondition, bool complicationsExperienced, string complicationNotes, string deliveryConductedBy, int createdBy)
        {
            PatientMasterVisitId = patientMasterVisitId;
            ProfileId = profileId;
            DurationOfLabour = labourDuration;
            DateOfDelivery = deliveryDate;
            TimeOfDelivery = deliveryTime;
            ModeOfDelivery = deliveryMode;
            PlacentaComplete = placentaComplete;
            BloodLoss = bloodLoss;
            MotherCondition = motherCondition;
            DeliveryComplicationsExperienced = complicationsExperienced;
            DeliveryComplicationNotes = complicationNotes;
            DeliveryConductedBy = deliveryConductedBy;
            CreatedBy = createdBy;
            CreateDate = DateTime.Now;

        }
        public int Id { get; private set; }
        public int PatientMasterVisitId { get; private set; }
        public int ProfileId { get; private set; }
        public int DurationOfLabour { get; private set; }
        public DateTime DateOfDelivery { get; private set; }
        public DateTime TimeOfDelivery { get; private set; }
        public int? ModeOfDelivery { get; private set; }
        public int? PlacentaComplete { get; private set; }
        public int? BloodLossCapacity { get; private set; }
        public int ? BloodLossClassificationId { get; set; }
        public int? MotherCondition { get; private set; }
        public bool DeliveryComplicationsExperienced { get; private set; }
        public string DeliveryComplicationNotes { get; private set; }
        public string DeliveryConductedBy { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTime CreateDate { get; private set; }
        public string AuditData { get; private set; }

    }
}
