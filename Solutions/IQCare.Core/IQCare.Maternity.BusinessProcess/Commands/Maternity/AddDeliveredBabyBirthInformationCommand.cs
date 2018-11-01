using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Commands.Maternity
{
    public class AddDeliveredBabyBirthInformationCommand : IRequest<Result<DeliveredBabyBirthInfoResult>>
    {
        public int PatientDeliveryInformationId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public decimal? BirthWeight { get; set; }
        public int? Sex { get; set; }
        public int? DeliveryOutcome { get; set; }
        public bool ResuscitationDone { get; set; }
        public bool BirthDeformity { get; set; }
        public bool TeoGiven { get; set; }
        public bool BreastFedWithinHour { get; set; }
        public string BirthNotificationNumber { get; set; }
        public string Comment { get; set; }
        public int CreatedBy { get; set; }
        public List<ApgrarScore> ApgarScores { get; set; }
    }

    public class AddDeliveredBabyBirthInfoCollectionCommand : IRequest<Result<DeliveredBabyBirthInfoResult>>
    {
        public List<AddDeliveredBabyBirthInformationCommand> DeliveredBabyBirthInfoCollection { get; set; }
    }

   
    public class ApgrarScore
    {
        public int ApgarScoreId { get; set; }
        public string ApgarScoreType { get; set; }
        public int Score { get; set; }
    }

    public class DeliveredBabyBirthInfoResult
    {
        public int DeliveredBabyBirthInfoId { get; set; }
        public int PatientDeliveryInformationId { get;  set; }
    }
}
