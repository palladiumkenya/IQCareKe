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
    public class AddHeiPatintIcfCommandHandler: IRequestHandler<AddPatientIcfCommand, Result<HeiPatientIcf>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public AddHeiPatintIcfCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<HeiPatientIcf>> Handle(AddPatientIcfCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    await _unitOfWork.Repository<HeiPatientIcf>().AddAsync(request.HeiPatientIcf);
                    await _unitOfWork.SaveAsync();
                    var response = request.HeiPatientIcf;
                    return Result<HeiPatientIcf>.Valid(request.HeiPatientIcf);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<HeiPatientIcf>.Invalid(e.Message);
                }
            }
        }
    }
}