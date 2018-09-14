using IQCare.Library;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.BusinessProcess.Commands.PatientHeiFeeding;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.heiFeeding
{
    public class GetHeiFeedingCommandHandler: IRequestHandler<GetHeiFeedingCommand,Result<HeiFeedingViewModel>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetHeiFeedingCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<HeiFeedingViewModel>> Handle(GetHeiFeedingCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    HeiFeeding result = await _unitOfWork.Repository<HeiFeeding>().Get(x => x.PatientId == request.PatientId).FirstOrDefaultAsync();
                    HeiFeedingViewModel heiFeedingView = new HeiFeedingViewModel();;
                    if (result != null)
                    {
                        heiFeedingView.Id = result.Id;
                        heiFeedingView.PatientId = result.PatientId;
                        heiFeedingView.PatientMasterVisitId = result.PatientMasterVisitId;
                        heiFeedingView.FeedingModeId = result.FeedingModeId;
                    }

                    return   Result<HeiFeedingViewModel>.Valid(heiFeedingView);
                                   
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<HeiFeedingViewModel>.Invalid(e.Message);
                }
            }
        }
    }
}
