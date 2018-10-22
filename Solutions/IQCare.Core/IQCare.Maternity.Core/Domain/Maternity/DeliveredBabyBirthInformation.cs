using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class DeliveredBabyBirthInformation
    {

 
        public DeliveredBabyBirthInformation()
        {

        }

        public int Id { get; private set; }
        public int PatientDeliveryInformationId { get; private set; }
        public int PatientMasterVisitId { get; private set; }
        public decimal ? BirthWeight { get; private set; }
        public int ? Sex { get; private set; }
        public int ? DeliveryOutcome { get; private set; }
        public bool ResuscitationDone { get; private set; }
        public bool BirthDeformity { get; private set; }
        public bool TeoGiven { get; private set; }
        public bool BreastFedWithinHour { get; private set; }
        public string BirthNotificationNumber { get; private set; }
        public string Comment { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTime CreateDate { get; private set; }
        public string AuditData { get; private set; }
        public bool DeleteFlag { get; private set; }
    }
}
