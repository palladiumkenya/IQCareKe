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
        public PatientDeliveryInformation(int patientMasterVisitId, int profileId, string labourDuration, DateTime deliveryDate, DateTime deliveryTime, int? deliveryMode, int? placentaComplete, int? bloodLossCapacity, int? bloodLossClassification, int? motherCondition, int ? complicationsExperienced, string complicationNotes, string deliveryConductedBy, int createdBy, int ? maternalDeathAudited, DateTime? maternalDeathAuditDate)
        {
            PatientMasterVisitId = patientMasterVisitId;
            ProfileId = profileId;
            DurationOfLabour = labourDuration;
            DateOfDelivery = deliveryDate;
            TimeOfDelivery = deliveryTime;
            ModeOfDelivery = deliveryMode;
            PlacentaComplete = placentaComplete;
            BloodLossCapacity = bloodLossCapacity;
            MotherCondition = motherCondition;
            DeliveryComplicationsExperienced = complicationsExperienced;
            DeliveryComplicationNotes = complicationNotes;
            DeliveryConductedBy = deliveryConductedBy;
            BloodLossClassification = bloodLossClassification;
            MaternalDeathAuditDate = maternalDeathAuditDate;
            MaternalDeathAudited = maternalDeathAudited;
            CreatedBy = createdBy;
            CreateDate = DateTime.Now;
        }

        public int Id { get; private set; }
        public int PatientMasterVisitId { get; private set; }
        public int ProfileId { get; private set; }
        public string DurationOfLabour { get; private set; }
        public DateTime DateOfDelivery { get; private set; }
        public DateTime TimeOfDelivery { get; private set; }
        public int? ModeOfDelivery { get; private set; }
        public int? PlacentaComplete { get; private set; }
        public int? BloodLossCapacity { get; private set; }
        public int? BloodLossClassification { get; set; }
        public int? MotherCondition { get; private set; }
        public int ? DeliveryComplicationsExperienced { get; private set; }
        public string DeliveryComplicationNotes { get; private set; }
        public string DeliveryConductedBy { get; private set; }
        public int ? MaternalDeathAudited { get; private set; }
        public DateTime? MaternalDeathAuditDate { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTime CreateDate { get; private set; }
        public string AuditData { get; private set; }
        public bool DeleteFlag { get; set; }

    }
}
