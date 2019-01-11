using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models.Views;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class GetPatientScreeningByPatientIdCommandHandler : IRequestHandler<GetPatientScreeningByPatientIdCommand, Result<List<PmtctPatientScreeningView>>>
    {

        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPatientScreeningByPatientIdCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<List<PmtctPatientScreeningView>>> Handle(GetPatientScreeningByPatientIdCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<PmtctPatientScreeningView> patientScreeningViews = await _unitOfWork
                        .Repository<PmtctPatientScreeningView>().Get(x =>
                            x.PatientId == request.PatientId)
                        .ToListAsync();
                    return Result<List<PmtctPatientScreeningView>>.Valid(patientScreeningViews);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<List<PmtctPatientScreeningView>>.Invalid(e.Message);
                }
            }
        }
    }
}