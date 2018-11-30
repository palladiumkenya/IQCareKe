using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiProfile;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiProfile
{
    public class GetHeiProfileCommandHandler : IRequestHandler<GetHeiProfileCommand, Result<PatientHeiProfile>>
  {
      private readonly IPmtctUnitOfWork _unitOfWork;

      public GetHeiProfileCommandHandler(IPmtctUnitOfWork unitOfWork)
      {
          _unitOfWork = unitOfWork;
      }

        public async Task<Result<PatientHeiProfile>> Handle(GetHeiProfileCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                   var heiProfile= await  _unitOfWork.Repository<PatientHeiProfile>()
                        .Get(x => x.PatientId == request.PatientId && !x.DeleteFlag).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                    return Result<PatientHeiProfile>.Valid(heiProfile);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<PatientHeiProfile>.Invalid(e.Message);
                }
            }
        }
    }
}
