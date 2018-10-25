using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
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
    public class AddPatientDeliveryInfoCommandHandler : IRequestHandler<AddMaternalPatientDeliveryInfoCommand, Result<AddPatientDeliveryInfoResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<AddPatientDeliveryInfoCommandHandler>();

        public AddPatientDeliveryInfoCommandHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<AddPatientDeliveryInfoResponse>> Handle(AddMaternalPatientDeliveryInfoCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var patientDelivery = _mapper.Map<PatientDeliveryInformation>(request);

                await _maternityUnitOfWork.Repository<PatientDeliveryInformation>().AddAsync(patientDelivery);
                await _maternityUnitOfWork.SaveAsync();

                return Result<AddPatientDeliveryInfoResponse>.Valid(new AddPatientDeliveryInfoResponse
                {
                    PatientDeliveryId = patientDelivery.Id,
                    PatientMasterVisitId = request.PatientMasterVisitId,
                    ProfileId = request.ProfileId
                });
            }
            catch (Exception ex)
            {
                string error = $"An error occured while capturing patient delivery information for Master visit {request.PatientMasterVisitId}";
                logger.Error(ex, error);

                return Result<AddPatientDeliveryInfoResponse>.Invalid(error);
            }
        }
    }
}
