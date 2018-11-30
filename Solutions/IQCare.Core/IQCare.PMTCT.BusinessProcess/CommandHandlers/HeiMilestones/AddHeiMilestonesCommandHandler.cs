using System;
using System.Collections.Generic;
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
    public class AddHeiMilestonesCommandHandler: IRequestHandler<AddMilestoneCommand,Result<AddMilestoneResponse>>
   {
       private readonly IPmtctUnitOfWork _unitOfWork;

       public AddHeiMilestonesCommandHandler(IPmtctUnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork;
       }

        public async Task<Result<AddMilestoneResponse>> Handle(AddMilestoneCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<HEIMilestone> heiMilestones= new List<HEIMilestone>();

                    foreach (var milestone in request.PatientMilestone)
                    {
                       HEIMilestone milestoneItem = new HEIMilestone()
                        {
                            PatientId = milestone.PatientId,
                            PatientMasterVisitId = milestone.PatientMasterVisitId,
                            TypeAssessedId = milestone.TypeAssessedId,
                            AchievedId = milestone.AchievedId,
                            StatusId = milestone.StatusId,
                            Comment = milestone.Comment,
                            CreatedBy = milestone.CreatedBy,
                            CreateDate = milestone.CreateDate,
                            DeleteFlag = milestone.DeleteFlag,
                            DateAssessed = milestone.DateAssessed
                       };
                        heiMilestones.Add(milestoneItem);
                    }

                    await _unitOfWork.Repository<HEIMilestone>().AddRangeAsync(heiMilestones);
                    await _unitOfWork.SaveAsync();
                    return Result<AddMilestoneResponse>.Valid(new AddMilestoneResponse()
                    {
                        Message = "Milestone Added Successfully"
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<AddMilestoneResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
