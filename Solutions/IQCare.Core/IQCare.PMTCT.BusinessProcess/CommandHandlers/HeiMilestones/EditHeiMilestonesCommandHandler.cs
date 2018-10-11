using System;
using System.Linq;
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
    public class EditHeiMilestonesCommandHandler: IRequestHandler<EditMilestoneCommand,Result<EditMilestoneResponse>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public EditHeiMilestonesCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<EditMilestoneResponse>> Handle(EditMilestoneCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    HEIMilestone milestone = _unitOfWork.Repository<HEIMilestone>().Get(x =>
                        x.PatientId == request.PatientMilestone.PatientId && x.PatientMasterVisitId ==
                        request.PatientMilestone.PatientMasterVisitId).FirstOrDefault();
                    if (milestone != null)
                    {
                        milestone.Achieved = request.PatientMilestone.Achieved;
                        milestone.Comment = request.PatientMilestone.Comment;
                        milestone.Status = request.PatientMilestone.Status;
                        milestone.TypeAssessed = request.PatientMilestone.TypeAssessed;
                    }

                     _unitOfWork.Repository<HEIMilestone>().Update(milestone);
                    await _unitOfWork.SaveAsync();
                    return Result<EditMilestoneResponse>.Valid(new EditMilestoneResponse()
                    {
                        Message = "Milestone Edited Successfully"
                    });

                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<EditMilestoneResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
