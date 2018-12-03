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
    public class GetPatientCounsellingViewCommandHandler: IRequestHandler<GetPatientCounselingViewCommand, Result<List<PatientCounsellingView>>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPatientCounsellingViewCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<List<PatientCounsellingView>>> Handle(GetPatientCounselingViewCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<PatientCounsellingView> counselingView = await _unitOfWork.Repository<PatientCounsellingView>().Get(x =>
                        x.PatientMasterVisitId == request.PatientMasterVisitId && x.PatientId == request.PatientId).ToListAsync();
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