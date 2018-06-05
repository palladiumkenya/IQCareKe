using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Encounter
{
    public class AddAppStoreCommand : IRequest<Result<AddAppStoreResponse>>
    {
        public int? PersonId { get; set; }
        public int? PatientId { get; set; }
        public int? PatientMasterVisitId { get; set; }
        public int? EncounterId { get; set; }
        public int AppStateId { get; set; }
        public string AppStateObject { get; set; }
    }

    public class AddAppStoreResponse
    {
        public bool IsSavedSuccessfully { get; set; }
    }
}