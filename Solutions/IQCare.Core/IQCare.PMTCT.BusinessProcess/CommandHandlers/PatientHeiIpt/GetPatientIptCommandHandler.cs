using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiIpt;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.PatientHeiIpt
{
    public class GetPatientIptCommandHandler: IRequestHandler<GetHeiPatientIptCommand,Result<List<PatientIpt>>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPatientIptCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<PatientIpt>>> Handle(GetHeiPatientIptCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                  List<PatientIpt> patientIpt = await _unitOfWork.Repository<PatientIpt>()
                        .Get(x => x.PatientId == request.PatientId && !x.DeleteFlag).ToListAsync();
                   
                                        return Result<List<PatientIpt>>.Valid(patientIpt);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<List<PatientIpt>>.Invalid(e.Message);
                }
            }
        }
    }
}