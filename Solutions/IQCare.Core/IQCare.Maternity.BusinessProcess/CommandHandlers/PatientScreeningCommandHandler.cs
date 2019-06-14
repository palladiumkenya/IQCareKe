using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.Core.Domain.PNC;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class PatientScreeningCommandHandler : IRequestHandler<PatientScreeningCommand, Result<AddPatientScreeningResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<PatientPartnerTestingCommandHandler>();

        public PatientScreeningCommandHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _maternityUnitOfWork = maternityUnitOfWork;
        }

        public async Task<Result<AddPatientScreeningResponse>> Handle(PatientScreeningCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patientScreening = _mapper.Map<PatientScreening>(request);
                await _maternityUnitOfWork.Repository<PatientScreening>().AddAsync(patientScreening);
                await _maternityUnitOfWork.SaveAsync();

                return Result<AddPatientScreeningResponse>.Valid(new AddPatientScreeningResponse
                {
                    IsScreeningDone = true
                });
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occured while adding patient patient screening info {request.PatientMasterVisitId}");
                return Result<AddPatientScreeningResponse>.Invalid(ex.Message);
            }
        }
    }
}