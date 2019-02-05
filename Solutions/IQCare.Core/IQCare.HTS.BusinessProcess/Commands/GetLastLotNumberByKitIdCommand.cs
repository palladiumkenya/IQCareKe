using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class GetLastLotNumberByKitIdCommand : IRequest<Result<Core.Model.Testing>>
    {
        public int KitId { get; set; }
    }
}