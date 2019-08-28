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
        public int Id { get;  set; }
        public int PatientDeliveryInformationId { get;  set; }
        public int PatientMasterVisitId { get;  set; }
        public decimal ? BirthWeight { get;  set; }
        public int ? Sex { get;  set; }
        public int ? DeliveryOutcome { get;  set; }
        public bool ResuscitationDone { get;  set; }
        public bool BirthDeformity { get;  set; }
        public bool TeoGiven { get;  set; }
        public bool BreastFedWithinHour { get;  set; }
        public string BirthNotificationNumber { get;  set; }
        public string Comment { get;  set; }
        public int CreatedBy { get;  set; }
        public DateTime CreateDate { get;  set; }
        public string AuditData { get;  set; }
        public bool DeleteFlag { get;  set; }

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
