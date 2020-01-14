using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Library;
using IQCare.Pharm.Core.Models;

namespace IQCare.Pharm.BusinessProcess.Commands.PatientPharmacy
{
  public   class GetPatientPharmacyVisitCommand :IRequest<Result<List<PharmacyVisit>>>
    {

        public int PatientId { get; set; }
    }
}
