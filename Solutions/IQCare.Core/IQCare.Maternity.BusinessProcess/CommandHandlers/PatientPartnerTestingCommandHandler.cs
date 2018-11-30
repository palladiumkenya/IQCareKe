using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.Core.Domain.PNC;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;



namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class PatientPartnerTestingCommandHandler : IRequestHandler<PatientPartnerTestingCommand, Result<PatientPatnerTestingResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<PatientPartnerTestingCommandHandler>();

        public PatientPartnerTestingCommandHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _maternityUnitOfWork = maternityUnitOfWork;
        }
        public async Task<Result<PatientPatnerTestingResponse>> Handle(PatientPartnerTestingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patientPatnerTesting = _mapper.Map<PatientPartnerTesting>(request);
                await _maternityUnitOfWork.Repository<PatientPartnerTesting>().AddAsync(patientPatnerTesting);
                await _maternityUnitOfWork.SaveAsync();

                return Result<PatientPatnerTestingResponse>.Valid(new PatientPatnerTestingResponse
                {
                    PatientId = patientPatnerTesting.Id
                });
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occured while adding patient patner testing info {request.PatientMasterVisitId}");
                return Result<PatientPatnerTestingResponse>.Invalid(ex.Message);
            }
        }
    }
}