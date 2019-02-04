using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class AddDeliveredBabyBirthInfomationCommandHandler :
        IRequestHandler<AddDeliveredBabyBirthInformationCommand, Result<DeliveredBabyBirthInfoResult>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<AddDeliveredBabyBirthInfomationCommandHandler>();
        public AddDeliveredBabyBirthInfomationCommandHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<DeliveredBabyBirthInfoResult>> Handle(AddDeliveredBabyBirthInformationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.PatientDeliveryInformationId == default(int))
                {
                    var patientDeliveryInfo = _maternityUnitOfWork.Repository<PatientDeliveryInformation>()
                        .Get(x => x.PatientMasterVisitId == request.PatientMasterVisitId).FirstOrDefault();

                    request.PatientDeliveryInformationId = patientDeliveryInfo != null ? patientDeliveryInfo.Id : 0;
                }

                var deliveredBabyBirthInformation = _mapper.Map<DeliveredBabyBirthInformation>(request);

                await _maternityUnitOfWork.Repository<DeliveredBabyBirthInformation>().AddAsync(deliveredBabyBirthInformation);

                if (request.ApgarScores != null)
                {
                    var apgarScores = request.ApgarScores
                        .Select(x => new DeliveredBabyApgarScore(x.ApgarScoreId, deliveredBabyBirthInformation.Id, x.Score)).ToList();

                    await _maternityUnitOfWork.Repository<DeliveredBabyApgarScore>().AddRangeAsync(apgarScores);
                }

                await _maternityUnitOfWork.SaveAsync();

                return Result<DeliveredBabyBirthInfoResult>.Valid(new DeliveredBabyBirthInfoResult
                {
                    DeliveredBabyBirthInfoId = deliveredBabyBirthInformation.Id,
                    PatientDeliveryInformationId = request.PatientDeliveryInformationId
                });
            }

            catch (Exception ex)
            {
                string errorMessage = $"An error occured while adding delivered baby birth info for patientmastervisitId {request.PatientMasterVisitId}";
                logger.Error(ex, errorMessage);

                return Result<DeliveredBabyBirthInfoResult>.Invalid(errorMessage);
            }
        }
    }
}
