using System.Collections.Generic;
using IQCare.Library;
using IQCare.Maternity.Core.Domain.Maternity;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Queries.Maternity
{
    public class GetPatientDrugAdministrationByVisitInfoQuery : IRequest<Result<List<PatientDrugAdministrationView>>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
}