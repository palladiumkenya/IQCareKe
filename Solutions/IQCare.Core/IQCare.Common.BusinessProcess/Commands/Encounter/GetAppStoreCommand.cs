using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Encounter
{
    public class GetAppStoreCommand : IRequest<Result<GetAppStoreResponse>>
    {
        public int? PersonId { get; set; }
        public int? PatientId { get; set; }
        public int? PatientMasterVisitId { get; set; }
        public int? EncounterId { get; set; }
    }

    public class GetAppStoreResponse
    {
        public List<AppStateStore> StateStore { get; set; }
    }
}