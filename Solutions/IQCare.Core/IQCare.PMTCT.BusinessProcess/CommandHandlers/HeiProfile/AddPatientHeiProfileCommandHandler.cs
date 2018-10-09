using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiProfile;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiProfile
{

    public class AddPatientHeiProfileCommandHandler: IRequestHandler<AddProfileCommand,Result<PatientHeiProfile>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public AddPatientHeiProfileCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientHeiProfile>> Handle(AddProfileCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {

                    await _unitOfWork.Repository<PatientHeiProfile>().AddAsync(request.PatientHeiProfile);
                    await _unitOfWork.SaveAsync();
                    var ret = request.PatientHeiProfile;
                    return Result<PatientHeiProfile>.Valid(request.PatientHeiProfile);
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
