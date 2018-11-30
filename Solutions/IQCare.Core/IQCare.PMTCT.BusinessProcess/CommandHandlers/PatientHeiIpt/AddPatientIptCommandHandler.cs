using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiIpt;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.PatientHeiIpt
{
    public class AddPatientIptCommandHandler : IRequestHandler<AddHeiPatientIptCommand,Result<PatientIpt>>
    {
        private IPmtctUnitOfWork _unitOfWork;

        public AddPatientIptCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientIpt>> Handle(AddHeiPatientIptCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                  await _unitOfWork.Repository<PatientIpt>().AddAsync(request.PatientIpt);
                    await _unitOfWork.SaveAsync();
                    return Result<PatientIpt>.Valid(request.PatientIpt);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<PatientIpt>.Invalid(e.Message);
                }
            }
        }
    }
}