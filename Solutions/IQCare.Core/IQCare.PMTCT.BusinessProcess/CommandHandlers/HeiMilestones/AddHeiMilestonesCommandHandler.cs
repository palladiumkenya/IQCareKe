using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiMilestones;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiMilestones
{
    public class AddHeiMilestonesCommandHandler: IRequestHandler<AddMilestoneCommand,Result<PatientMilestone>>
   {
       private readonly IPmtctUnitOfWork _unitOfWork;

       public AddHeiMilestonesCommandHandler(IPmtctUnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork;
       }

        public async Task<Result<PatientMilestone>> Handle(AddMilestoneCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    await _unitOfWork.Repository<PatientMilestone>().AddAsync(request.PatientMilestone);
                    await _unitOfWork.SaveAsync();
                    return Result<PatientMilestone>.Valid(request.PatientMilestone);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<PatientMilestone>.Invalid(e.Message);
                }
            }
        }
    }
}
