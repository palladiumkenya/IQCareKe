using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class GetPregnancyIndicatorCommandHandler : IRequestHandler<GetPregnancyIndicatorCommand, Result<PregnancyIndicator>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;

        public GetPregnancyIndicatorCommandHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PregnancyIndicator>> Handle(GetPregnancyIndicatorCommand request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    var result = await _maternityUnitOfWork.Repository<PregnancyIndicator>().Get(x =>
                        x.PatientId == request.PatientId && x.PatientMasterVisitId == request.PatientMasterVisitId &&
                        x.DeleteFlag == false).FirstOrDefaultAsync();

                    return Result<PregnancyIndicator>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error($"An error occured while trying to fetch pregnancyindicator for patientId: {request.PatientId} and patientMasterVisitId: {request.PatientMasterVisitId}. Exception: {e.Message} {e.InnerException}");
                    return Result<PregnancyIndicator>.Invalid($"An error occured while trying to fetch pregnancyindicator for patientId: {request.PatientId} and patientMasterVisitId: {request.PatientMasterVisitId}.");
                }
            }
        }
    }
}