using IQCare.Common.Core.Models;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System.Collections.Generic;
using IQCare.Library;

namespace IQCare.PMTCT.BusinessProcess.Commands.DrugAdministration
{
    public class GetDrugAdministrationCommand:IRequest<Result<List<PatientDrugAdministration>>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }

}
