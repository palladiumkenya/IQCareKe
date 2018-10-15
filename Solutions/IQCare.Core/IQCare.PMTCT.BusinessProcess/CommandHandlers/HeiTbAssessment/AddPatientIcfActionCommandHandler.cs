using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiTbAssessment;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiTbAssessment
{
    public class AddPatientIcfActionCommandHandler: IRequestHandler<AddPatientIcfActionCommand, Result<HEiPatientIcfAction>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public AddPatientIcfActionCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<HEiPatientIcfAction>> Handle(AddPatientIcfActionCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    await _unitOfWork.Repository<HEiPatientIcfAction>().AddAsync(request.HEiPatientIcfAction);
                    await _unitOfWork.SaveAsync();
                    return Result<HEiPatientIcfAction>.Valid(request.HEiPatientIcfAction);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<HEiPatientIcfAction>.Invalid(e.Message);
                }
            }
        }
    }
}