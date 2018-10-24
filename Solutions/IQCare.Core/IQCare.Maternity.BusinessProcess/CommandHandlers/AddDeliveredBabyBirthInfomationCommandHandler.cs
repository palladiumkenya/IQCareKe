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
        ILogger logger = Log.ForContext<AddDeliveredBabyBirthInfomationCommandHandler>();
        public AddDeliveredBabyBirthInfomationCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }

        public async Task<Result<DeliveredBabyBirthInfoResult>> Handle(AddDeliveredBabyBirthInformationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deliveredBayBirthInformation = new DeliveredBabyBirthInformation(request.PatientDeliveryInformationId, request.PatientMasterVisitId, request.BirthWeight, request.Sex, request.DeliveryOutcome, request.ResuscitationDone, request.TeoGiven, request.BreastFedWithinHour, request.BirthNotificationNumber, request.Comment, request.CreatedBy);

                await _maternityUnitOfWork.Repository<DeliveredBabyBirthInformation>().AddAsync(deliveredBayBirthInformation);

                if (request.ApgarScores != null)
                {
                    var apgarScores = request.ApgarScores.Select(x => new DeliveredBabyApgarScore(x.ApgarScoreId, deliveredBayBirthInformation.Id, x.Score)).ToList();

                    await _maternityUnitOfWork.Repository<DeliveredBabyApgarScore>().AddRangeAsync(apgarScores);
                }

                await _maternityUnitOfWork.SaveAsync();

                return Result<DeliveredBabyBirthInfoResult>.Valid(new DeliveredBabyBirthInfoResult
                {
                    DeliveredBabyBirthInfoId = deliveredBayBirthInformation.Id,
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
