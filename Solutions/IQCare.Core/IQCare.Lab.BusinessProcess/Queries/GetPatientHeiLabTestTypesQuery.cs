using System.Collections.Generic;
using IQCare.Lab.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Lab.BusinessProcess.Queries
{
    public class GetPatientHeiLabTestTypesQuery : IRequest<Result<List<HeiLabTests>>>
    {
        public int PatientId { get; set; }
    }
}