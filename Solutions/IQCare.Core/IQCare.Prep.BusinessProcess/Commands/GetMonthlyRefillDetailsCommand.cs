using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.Prep.Core.Models;
using MediatR;

namespace IQCare.Prep.BusinessProcess.Commands
{
    public class GetMonthlyRefillDetailsCommand:IRequest<Result<RefillResponse>>
    {

        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }

        public int ServiceAreaId { get; set; }
    }

    public class RefillResponse
    {
        public List<MonthlyRefillDetailsResponse> refilldetails { get; set; }

        public PatientClinicalNotes clinicalnote { get; set; }
    }

    public class MonthlyRefillDetailsResponse
    {
        public int PatientId { get; set; }



        public int PatientMasterVisitId { get; set; }

        public int? MasterId { get; set; }

        public int ItemId { get; set; }

        public string Comment { get; set; }


    }

    
}
