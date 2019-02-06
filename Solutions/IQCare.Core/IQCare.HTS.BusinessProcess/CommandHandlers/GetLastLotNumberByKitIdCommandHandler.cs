using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Testing = IQCare.HTS.Core.Model.Testing;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class GetLastLotNumberByKitIdCommandHandler : IRequestHandler<GetLastLotNumberByKitIdCommand, Result<Core.Model.Testing>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;

        public GetLastLotNumberByKitIdCommandHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<Core.Model.Testing>> Handle(GetLastLotNumberByKitIdCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var kitTesting = await _unitOfWork.Repository<Core.Model.Testing>()
                        .Get(x => x.KitId == request.KitId && x.DeleteFlag == false)
                        .OrderByDescending(y => y.CreateDate).FirstOrDefaultAsync();

                    return Result<Testing>.Valid(kitTesting);
                }
                catch (Exception e)
                {
                    Log.Error($"Error fetching kits for kitId: {request.KitId}" + e.Message);
                    return Result<Testing>.Invalid($"Error fetching kits for kitId: {request.KitId}");
                }
            }
        }
    }
}