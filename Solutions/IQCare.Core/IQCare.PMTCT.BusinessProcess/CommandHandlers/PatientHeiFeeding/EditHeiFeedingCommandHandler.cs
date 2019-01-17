using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.PatientHeiFeeding;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.PatientHeiFeeding
{
    public class EditHeiFeedingCommandHandler: IRequestHandler<EditHeiFeedingCommand, Result<EditHeiFeedingCommandResult>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public EditHeiFeedingCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<EditHeiFeedingCommandResult>> Handle(EditHeiFeedingCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var heiFeeding = await _unitOfWork.Repository<HeiFeeding>().FindByIdAsync(request.heiFeeding.Id);
                    if(heiFeeding==null)
                        return Result<EditHeiFeedingCommandResult>.Invalid($"Could not find heifeeding for Id: {request.heiFeeding.Id}");

                    heiFeeding.FeedingModeId = request.heiFeeding.FeedingModeId;
                    _unitOfWork.Repository<HeiFeeding>().Update(heiFeeding);
                    await _unitOfWork.SaveAsync();

                    return Result<EditHeiFeedingCommandResult>.Valid(new EditHeiFeedingCommandResult()
                    {
                        Id = heiFeeding.Id
                    });
                }
                catch (Exception e)
                {
                    Log.Error($"Error updating heifeeding for Id: {request.heiFeeding.Id}. {e.Message} {e.InnerException}");
                    return Result<EditHeiFeedingCommandResult>.Invalid($"Error updating heifeeding for Id: {request.heiFeeding.Id}.");
                }
            }
        }
    }
}
