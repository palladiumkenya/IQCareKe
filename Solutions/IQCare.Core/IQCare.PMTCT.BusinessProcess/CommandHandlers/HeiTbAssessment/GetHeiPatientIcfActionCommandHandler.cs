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
    public class GetHeiPatientIcfActionCommandHandler: IRequestHandler<GetHeiPatientIcfActionCommand,Result<List<HEiPatientIcfAction>>>
    {
        private IPmtctUnitOfWork _unitOfWork;

        public GetHeiPatientIcfActionCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<HEiPatientIcfAction>>> Handle(GetHeiPatientIcfActionCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                   List<HEiPatientIcfAction> icfAction= await  _unitOfWork.Repository<HEiPatientIcfAction>()
                        .Get(x => x.PatientId == request.PatientId && !x.DeleteFlag).ToListAsync();
                    return Result<List<HEiPatientIcfAction>>.Valid(icfAction);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<List<HEiPatientIcfAction>>.Invalid(e.Message);
                }
            }
        }
    }
}