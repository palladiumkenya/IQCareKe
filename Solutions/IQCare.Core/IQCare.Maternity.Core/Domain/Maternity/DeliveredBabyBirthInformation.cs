using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class DeliveredBabyBirthInformation
    {

 
        public DeliveredBabyBirthInformation()
        {

        }

        public DeliveredBabyBirthInformation(int patientDeliveryInformationId, int masterVisitId, decimal ? birthWeight, 
            int ? sex, int ? deliveryOutcome, bool resuscitationDone, bool teoGiven, bool breastFestWithinHr, string birthNotificationNumber,
            string comment, int createdBy)
        {
            PatientDeliveryInformationId = patientDeliveryInformationId;
            PatientMasterVisitId = masterVisitId;
            BirthWeight = birthWeight;
            Sex = sex;
            DeliveryOutcome = deliveryOutcome;
            ResuscitationDone = resuscitationDone;
            TeoGiven = teoGiven;
            BreastFedWithinHour = breastFestWithinHr;
            BirthNotificationNumber = birthNotificationNumber;
            Comment = comment;
            CreatedBy = createdBy;
            CreateDate = DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public void Update(dynamic babyInfo)
        {
            PatientDeliveryInformationId = babyInfo.PatientDeliveryInformationId;
            BirthWeight = babyInfo.BirthWeight;
            Sex = babyInfo.Sex;
            DeliveryOutcome = babyInfo.DeliveryOutcome;
            ResuscitationDone = babyInfo.ResuscitationDone;
            TeoGiven = babyInfo.TeoGiven;
            BreastFedWithinHour = babyInfo.BreastFedWithinHour;
            BirthNotificationNumber = babyInfo.BirthNotificationNumber;
            Comment = babyInfo.Comment;
        }
    }
}
