using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Pharm.Core.Models;
using MediatR;
using IQCare.Library;

namespace IQCare.Pharm.BusinessProcess.Commands.PatientPharmacy
{
    public class GetPharmacyDrugBatchCommand :IRequest<Result<GetPharmacyDrugBatchResponse>>
    {
        public string Drug_Pk { get; set; }
    }

    public class GetPharmacyDrugBatchResponse
    {
            public List<DrugBatch> drugBatches { get; set; }
     }
}
