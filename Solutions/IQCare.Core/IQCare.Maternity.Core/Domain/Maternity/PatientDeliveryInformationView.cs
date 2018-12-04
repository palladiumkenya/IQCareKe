using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class PatientDeliveryInformationView
    {
        public PatientDeliveryInformationView()
        {

        }

        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ? PregnancyId { get; set; }
        public string DurationOfLabour { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public TimeSpan TimeOfDelivery { get; set; }
        public string ModeOfDelivery { get; set; }
        public string PlacentaComplete { get; set; }
        public int? BloodLossCapacity { get; set; }
        public string BloodLossClassification { get; set; }
        public string MotherCondition { get; set; }
        public string DeliveryComplicationsExperienced { get; set; }
        public string DeliveryComplicationNotes { get; set; }
        public string DeliveryConductedBy { get; set; }
        public string MaternalDeathAudited { get; set; }
        public DateTime? MaternalDeathAuditDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
