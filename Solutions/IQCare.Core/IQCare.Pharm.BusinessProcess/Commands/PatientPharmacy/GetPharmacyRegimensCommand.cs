using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;
using IQCare.Pharm.Core.Models;

namespace IQCare.Pharm.BusinessProcess.Commands.PatientPharmacy
{
    public class GetPharmacyRegimensCommand:IRequest<Result<GetPharmacyRegimensResponse>>
    {
        public string LookupName;
    }

    public class GetPharmacyRegimensResponse
    {
        public List<Regimen> Regimens { get; set; }
    }
}
