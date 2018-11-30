using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiTbAssessment;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiTbAssessment
{
    public class GetHeiPatientIcfCommandHandler: IRequestHandler<GetPatientIcfCommand,Result<List<HeiPatientIcf>>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetHeiPatientIcfCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<HeiPatientIcf>>> Handle(GetPatientIcfCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                   List<HeiPatientIcf>  heiPatientIcf= await _unitOfWork.Repository<HeiPatientIcf>()
                        .Get(x => x.PatientId == request.PatientId && !x.DeleteFlag).ToListAsync();
                    return Result<List<HeiPatientIcf>>.Valid(heiPatientIcf);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<List<HeiPatientIcf>>.Invalid(e.Message);
                }                  
            }
        }
    }
}