using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.BusinessProcess.Queries.Maternity
{
    public class GetPatientDischargeInfoQuery : IRequest<Result<List<PatientDischargeInfoViewModel>>>
    {
        public int PatientMasterVisitId { get; set; }


    }

    public class PatientDischargeInfoViewModel
    {
        public int Id { get; set; }
        public int? PatientMasterVisitId { get; set; }
        public string OutcomeStatus { get; set; }
        public DateTime? DateDischarged { get; set; }
        public string OutcomeDescription { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public bool DeleteFlag { get; set; }
    }

}
