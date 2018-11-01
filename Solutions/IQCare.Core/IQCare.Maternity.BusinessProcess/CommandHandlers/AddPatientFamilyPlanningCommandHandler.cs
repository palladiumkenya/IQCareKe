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
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    class AddPatientFamilyPlanningCommandHandler : IRequestHandler<AddPatientFamilyPlanningCommand, Result<AddFamilyPlaaningResultsResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<AddPatientFamilyPlanningCommandHandler>();
        public AddPatientFamilyPlanningCommandHandler(IMapper mapper, IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AddFamilyPlaaningResultsResponse>> Handle(AddPatientFamilyPlanningCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var familyPlanning = _mapper.Map<PatientFamilyPlanning>(request);
                await _maternityUnitOfWork.Repository<PatientFamilyPlanning>().AddAsync(familyPlanning);
                await _maternityUnitOfWork.SaveAsync();

                return Result<AddFamilyPlaaningResultsResponse>.Valid(new AddFamilyPlaaningResultsResponse
                {
                    PatientId = familyPlanning.Id
                });
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occured while adding patient family planning details {request.PatientId}");
                return Result<AddFamilyPlaaningResultsResponse>.Invalid(ex.Message);
            }
        }

    }


}
