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
    public class EditHeiMilestonesCommandHandler: IRequestHandler<EditMilestoneCommand,Result<PatientMilestone>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public EditHeiMilestonesCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientMilestone>> Handle(EditMilestoneCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientMilestone milestone = _unitOfWork.Repository<PatientMilestone>().Get(x =>
                        x.PatientId == request.PatientMilestone.PatientId && x.PatientMasterVisitId ==
                        request.PatientMilestone.PatientMasterVisitId).FirstOrDefault();
                    if (milestone != null)
                    {
                        milestone.Achieved = request.PatientMilestone.Achieved;
                        milestone.Comment = request.PatientMilestone.Comment;
                        milestone.Status = request.PatientMilestone.Status;
                        milestone.TypeAssessed = request.PatientMilestone.TypeAssessed;
                    }

                     _unitOfWork.Repository<PatientMilestone>().Update(milestone);
                    await _unitOfWork.SaveAsync();
                    return Result<PatientMilestone>.Valid(milestone);

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
