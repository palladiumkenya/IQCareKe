using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiIptWorkup;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiIptWorkup
{
    public class AddIptWorkupCommandHandler: IRequestHandler<AddHeiPatientIptWorkupCommand, Result<PatientIptWorkup>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public AddIptWorkupCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientIptWorkup>> Handle(AddHeiPatientIptWorkupCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                   await _unitOfWork.Repository<PatientIptWorkup>().AddAsync(request.PatientIptWorkup);
                    await _unitOfWork.SaveAsync();
                    return Result<PatientIptWorkup>.Valid(request.PatientIptWorkup);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<PatientIptWorkup>.Invalid(e.Message);
                }
            }
        }
    }
}