using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.Profile;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.Profile
{
    public class GetPatientProfileCommandHandler : IRequestHandler<GetPatientProfileCommand,Result<List<PatientProfile>>>
   {
       private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPatientProfileCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<PatientProfile>>> Handle(GetPatientProfileCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<PatientProfile> patientProfiles = await _unitOfWork.Repository<PatientProfile>()
                        .Get(x => x.PatientId == request.PatientId & x.PregnancyId == request.PregnancyId)
                        .ToListAsync();

                 return   Result<List<PatientProfile>>.Valid(patientProfiles);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<List<PatientProfile>>.Invalid(e.Message);
                }
            }
        }
    }
}
