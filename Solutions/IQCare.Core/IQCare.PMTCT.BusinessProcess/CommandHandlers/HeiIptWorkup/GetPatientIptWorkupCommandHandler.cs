using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiIptWorkup;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiIptWorkup
{
    public class GetPatientIptWorkupCommandHandler: IRequestHandler<GetHeiIptWorkupCommand, Result<List<PatientIptWorkup>>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPatientIptWorkupCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<PatientIptWorkup>>> Handle(GetHeiIptWorkupCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<PatientIptWorkup> patientIptWorkup = await _unitOfWork.Repository<PatientIptWorkup>()
                        .Get(x => x.PatientId == request.PatientId && !x.DeleteFlag).ToListAsync();
                    return Result<List<PatientIptWorkup>>.Valid(patientIptWorkup);

                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<List<PatientIptWorkup>>.Invalid(e.Message);
                }
            }
        }
    }
}