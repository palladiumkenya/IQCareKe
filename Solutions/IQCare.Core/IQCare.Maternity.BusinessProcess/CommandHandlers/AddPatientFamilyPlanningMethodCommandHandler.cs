using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;
using System;
using IQCare.Maternity.Core.Domain.PNC;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    class AddPatientFamilyPlanningMethodCommandHandler : IRequestHandler<AddPatientFamilyPlanningMethodCommand, Result<AddFamilyPlaaningMethodResultsResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<AddPatientFamilyPlanningCommandHandler>();
        public AddPatientFamilyPlanningMethodCommandHandler(IMapper mapper, IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AddFamilyPlaaningMethodResultsResponse>> Handle(AddPatientFamilyPlanningMethodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var familyPlanningMethod = _mapper.Map<PatientFamilyPlanningMethod>(request);
                await _maternityUnitOfWork.Repository<PatientFamilyPlanningMethod>().AddAsync(familyPlanningMethod);
                await _maternityUnitOfWork.SaveAsync();

                return Result<AddFamilyPlaaningMethodResultsResponse>.Valid(new AddFamilyPlaaningMethodResultsResponse
                {
                    PatientId = familyPlanningMethod.Id
                });
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occured while adding patient family planning methods {request.PatientId}");
                return Result<AddFamilyPlaaningMethodResultsResponse>.Invalid(ex.Message);
            }
        }

    }


}
