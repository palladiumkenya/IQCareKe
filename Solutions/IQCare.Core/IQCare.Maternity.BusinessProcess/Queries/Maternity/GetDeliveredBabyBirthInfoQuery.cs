using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Queries.Maternity
{
    public class GetDeliveredBabyBirthInfoQuery : IRequest<Result<List<DeliveredBabyBirthInfoViewModel>>>
    {
        public int PatientDeliveryInformationId { get; set; }

    }

    public class DeliveredBabyBirthInfoViewModel
    {
        public int Id { get;  set; }
        public int PatientDeliveryInformationId { get;  set; }
        public int PatientMasterVisitId { get;  set; }
        public decimal? BirthWeight { get;  set; }
        public string Sex { get;  set; }
        public  string DeliveryOutcome { get;  set; }
        public bool ResuscitationDone { get;  set; }
        public bool BirthDeformity { get;  set; }
        public bool TeoGiven { get;  set; }
        public bool BreastFedWithinHour { get;  set; }
        public string BirthNotificationNumber { get;  set; }
        public string Comment { get;  set; }
        public int CreatedBy { get;  set; }
        public DateTime DateCreated { get;  set; }
        public bool DeleteFlag { get;  set; }
        public string ApgarScores { get; set; }

    }
}
