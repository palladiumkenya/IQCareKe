using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiMilestones;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiMilestones
{
    public  class GetHeiMilestoneCommandHandler: IRequestHandler<GetPatientMilestoneCommand,Result<List<HEIMilestone>>>
  {
      private readonly IPmtctUnitOfWork _unitOfWork;

      public GetHeiMilestoneCommandHandler(IPmtctUnitOfWork unitOfWork)
      {
          _unitOfWork = unitOfWork;
      }

        public async Task<Result<List<HEIMilestone>>> Handle(GetPatientMilestoneCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                   List<HEIMilestone>  patientMilestone = await _unitOfWork.Repository<HEIMilestone>()
                        .Get(x => x.PatientId == request.PatientId && !x.DeleteFlag).ToListAsync();
                    return Result<List<HEIMilestone>>.Valid(patientMilestone);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<List<HEIMilestone>>.Invalid(e.Message);
                }
            }
        }
    }
}
