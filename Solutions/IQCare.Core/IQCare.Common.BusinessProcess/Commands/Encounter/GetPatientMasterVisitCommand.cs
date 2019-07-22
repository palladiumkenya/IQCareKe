using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.Encounter
{
   public  class GetPatientMasterVisitCommand : IRequest<Result<List<IQCare.Common.Core.Models.PatientMasterVisit>>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId{ get; set; }
    }
}
