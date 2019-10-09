using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Pharm.Core.Models;
using IQCare.Library;
using MediatR;
namespace IQCare.Pharm.BusinessProcess.Commands.PatientPharmacy
{
   public  class GetPharmacyCurrentRegimenCommand: IRequest<Result<GetPharmacyCurrentRegimenResponse>>
    {
        public int PatientId { get; set; }
    }

    public class GetPharmacyCurrentRegimenResponse
    {
        public List<PharmacyFields> pharmacyFields { get; set; }
    }
}
