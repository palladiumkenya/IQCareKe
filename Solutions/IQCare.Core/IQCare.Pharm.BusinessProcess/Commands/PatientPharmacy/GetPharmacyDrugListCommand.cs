using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Pharm.Core.Models;
using IQCare.Library;
using MediatR;
namespace IQCare.Pharm.BusinessProcess.Commands.PatientPharmacy
{
    public class GetPharmacyDrugListCommand :IRequest<Result<GetPharmacyDrugListResponse>>
    {

        public int pmscm { get; set; }

        public string tp { get; set; }

        public string filteritem { get; set; }
    }

    public class DrugListSearchQuery
    {
        public int pmscm { get; set; }

        public string tp { get; set; }

        public string filteritem { get; set; }
    }

    public class GetPharmacyDrugListResponse {

        public  List<DrugListPoco> DrugList {get;set;}
    }
    
}
