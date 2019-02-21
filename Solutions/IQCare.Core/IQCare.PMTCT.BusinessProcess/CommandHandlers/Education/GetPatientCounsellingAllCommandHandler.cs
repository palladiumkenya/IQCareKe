using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.Education;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.Education
{
    public class GetPatientCounsellingAllCommandHandler : IRequestHandler<GetPatientCounsellingAllCommand, Result<List<Core.Models.PatientCounsellingView>>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPatientCounsellingAllCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<List<PatientCounsellingView>>> Handle(GetPatientCounsellingAllCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<PatientCounsellingView> counselingView = await _unitOfWork.Repository<PatientCounsellingView>().Get(x =>
                        x.PatientId == request.PatientId && x.CounsellingTopicId>0).ToListAsync();
                    return Result<List<PatientCounsellingView>>.Valid(counselingView);
                }
                catch (Exception e)
                {

                    Log.Error(e.Message);
                    return Result<List<PatientCounsellingView>>.Invalid(e.Message);
                }
            }
        }
    }
}