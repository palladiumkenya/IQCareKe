using IQCare.Library;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System;
using System.Threading;
using IQCare.PMTCT.BusinessProcess.Commands.Profile;
using System.Threading.Tasks;
using Serilog;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.Profile
{
    public class GetPatientInitialProfileCommandHandler : IRequestHandler<GetPatientInitialProfileCommand, Result<PatientProfile>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPatientInitialProfileCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Result<PatientProfile>> Handle(GetPatientInitialProfileCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var patientProfiles = _unitOfWork.Repository<PatientProfile>()
                        .Get(x => x.PatientId == request.PatientId && !x.DeleteFlag).OrderBy(x => x.Id).AsEnumerable();

                    var patientProfile = request.PregnancyId.HasValue ?
                         patientProfiles.FirstOrDefault(x => x.PregnancyId == request.PregnancyId) : patientProfiles.FirstOrDefault();

                    patientProfile = patientProfile ?? new PatientProfile();

                    return Task.FromResult(Result<PatientProfile>.Valid(patientProfile));
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Task.FromResult(Result<PatientProfile>.Invalid(e.Message));
                }
            }
        }
    }
}
