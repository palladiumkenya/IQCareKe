using System;
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
    public  class GetHeiMilestoneCommandHandler: IRequestHandler<GetPatientMilestoneCommand,Result<PatientMilestone>>
  {
      private readonly IPmtctUnitOfWork _unitOfWork;

      public GetHeiMilestoneCommandHandler(IPmtctUnitOfWork unitOfWork)
      {
          _unitOfWork = unitOfWork;
      }

        public async Task<Result<PatientMilestone>> Handle(GetPatientMilestoneCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientMilestone patientMilestone = await _unitOfWork.Repository<PatientMilestone>()
                        .Get(x => x.PatientId == request.PatientId && !x.DeleteFlag).FirstOrDefaultAsync();
                    return Result<PatientMilestone>.Valid(patientMilestone);
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
