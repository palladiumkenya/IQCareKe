using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class DeliveredBabyBirthInfoView
    {
        public DeliveredBabyBirthInfoView()
        {

        }

        public int Id { get; set; }
        public int PatientDeliveryInformationId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public decimal? BirthWeight { get; set; }
        public string Sex { get; set; }
        public string DeliveryOutcome { get; set; }
        public bool ResuscitationDone { get; set; }
        public bool BirthDeformity { get; set; }
        public bool TeoGiven { get; set; }
        public bool BreastFedWithinHour { get; set; }
        public string BirthNotificationNumber { get; set; }
        public string Comment { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
