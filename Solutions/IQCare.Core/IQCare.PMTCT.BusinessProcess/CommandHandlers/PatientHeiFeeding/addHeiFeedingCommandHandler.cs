using IQCare.Library;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IQCare.PMTCT.BusinessProcess.Commands.PatientHeiFeeding;
using IQCare.PMTCT.Core.Models.HEI;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.PatientHeiFeeding
{
    public class GetHeiFeedingCommandHandler : IRequestHandler<AddHeiFeedingCommand, Result<AddHeiFeedingCommandResponse>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetHeiFeedingCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AddHeiFeedingCommandResponse>> Handle(AddHeiFeedingCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    HeiFeeding heiFeeding = new HeiFeeding()
                    {
                        PatientId = request.PatientId,
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        FeedingModeId = request.FeedingModeId,
                        CreatedBy = request.UserId,
                        CreateDate = DateTime.Now,
                        DeleteFlag = false
                    };

                    await _unitOfWork.Repository<HeiFeeding>().AddAsync(heiFeeding);
                    await _unitOfWork.SaveAsync();
                    return Result<AddHeiFeedingCommandResponse>.Valid(new AddHeiFeedingCommandResponse()
                    {
                        HeiFeedingId = heiFeeding.Id
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<AddHeiFeedingCommandResponse>.Invalid(e.Message);
                }
            }
        }

        //public async Task<Result<HeiFeedingViewModel>> Handle(AddHeiFeedingCommand request, CancellationToken cancellationToken)
        //{
        //    using (_unitOfWork)
        //    {
        //        try
        //        {
        //            HeiFeeding result = await _unitOfWork.Repository<HeiFeeding>().Get(x => x.PatientId == request.PatientId).FirstOrDefaultAsync();
        //            HeiFeedingViewModel heiFeedingView = new HeiFeedingViewModel(); ;
        //            if (result != null)
        //            {
        //                heiFeedingView.Id = result.Id;
        //                heiFeedingView.PatientId = result.PatientId;
        //                heiFeedingView.PatientMasterVisitId = result.PatientMasterVisitId;
        //                heiFeedingView.FeedingModeId = result.FeedingModeId;
        //            }

        //            return Result<HeiFeedingViewModel>.Valid(heiFeedingView);

        //        }
        //        catch (Exception e)
        //        {
        //            Log.Error(e.Message);
        //            return Result<HeiFeedingViewModel>.Invalid(e.Message);
        //        }
        //    }
        //}

    }
}
