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
    public class EditHeiProfileCommandHandler: IRequestHandler<EditProfileCommand,Result<PatientHeiProfile>>
  {
      private readonly IPmtctUnitOfWork _unitOfWork;

      public EditHeiProfileCommandHandler(IPmtctUnitOfWork unitOfWork)
      {
          _unitOfWork = unitOfWork;
      }

        public async Task<Result<PatientHeiProfile>> Handle(EditProfileCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientHeiProfile patientHeiProfile = _unitOfWork.Repository<PatientHeiProfile>().Get(x =>
                        x.PatientId == request.PatientHeiProfile.PatientId && x.PatientMasterVisitId ==
                        request.PatientHeiProfile.PatientMasterVisitId).FirstOrDefault();
                    if (patientHeiProfile != null)
                    {
                        patientHeiProfile.VisitDate = request.PatientHeiProfile.VisitDate;
                        patientHeiProfile.VisitType = request.PatientHeiProfile.VisitType;
                    }
                    await _unitOfWork.Repository<PatientHeiProfile>().AddAsync(patientHeiProfile);
                    await _unitOfWork.SaveAsync();
                    return Result<PatientHeiProfile>.Valid(patientHeiProfile);
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
