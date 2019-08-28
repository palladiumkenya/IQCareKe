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
                    var patientDeliveryInfo = _maternityUnitOfWork.Repository<PatientDeliveryInformationView>()
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

    public class UpdateDeliveredBabyBirthInfoCommandHandler : IRequestHandler<UpdateDeliveredBabyBirthInfoCommand, Result<object>>
    {
        private readonly IMaternityUnitOfWork _maternityUnitOfWork;

        public UpdateDeliveredBabyBirthInfoCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }

        public Task<Result<object>> Handle(UpdateDeliveredBabyBirthInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deliveredBabyInfo = _maternityUnitOfWork.Repository<DeliveredBabyBirthInformation>()
                        .Get(x => x.Id == request.DeliveredBabyBirthInformation.Id).SingleOrDefault();

                if (deliveredBabyInfo == null)
                    return Task.FromResult(Result<object>.Valid(new
                    {
                        Message = $"Delivered baby info not found for Id {request.DeliveredBabyBirthInformation.Id}"
                    }));

                // deliveredBabyInfo.Update(request.DeliveredBabyBirthInformation);
                deliveredBabyInfo.BirthDeformity = request.DeliveredBabyBirthInformation.BirthDeformity;
                deliveredBabyInfo.BirthNotificationNumber = request.DeliveredBabyBirthInformation.BirthNotificationNumber;
                deliveredBabyInfo.BirthWeight = request.DeliveredBabyBirthInformation.BirthWeight;
                deliveredBabyInfo.BreastFedWithinHour = request.DeliveredBabyBirthInformation.BreastFedWithinHour;
                deliveredBabyInfo.Comment = request.DeliveredBabyBirthInformation.Comment;
                deliveredBabyInfo.DeliveryOutcome = request.DeliveredBabyBirthInformation.DeliveryOutcome;
                deliveredBabyInfo.ResuscitationDone = request.DeliveredBabyBirthInformation.ResuscitationDone;
                deliveredBabyInfo.Sex = request.DeliveredBabyBirthInformation.Sex;
                deliveredBabyInfo.TeoGiven = request.DeliveredBabyBirthInformation.TeoGiven;

                _maternityUnitOfWork.Repository<DeliveredBabyBirthInformation>().Update(deliveredBabyInfo);
                _maternityUnitOfWork.Save();

                for (var i = 0; i < request.DeliveredBabyBirthInformation.ApgarScores.Count; i++)
                {
                    var score = _maternityUnitOfWork.Repository<DeliveredBabyApgarScore>().Get(x => x.DeliveredBabyBirthInformationId == request.DeliveredBabyBirthInformation.Id 
                    && x.ApgarScoreId == request.DeliveredBabyBirthInformation.ApgarScores[i].ApgarScoreId).ToList();

                    if(score.Count > 0)
                    {
                        score[0].Score = request.DeliveredBabyBirthInformation.ApgarScores[i].Score;
                        _maternityUnitOfWork.Repository<DeliveredBabyApgarScore>().Update(score[0]);
                        _maternityUnitOfWork.Save();
                    }
                }                    

                return Task.FromResult(Result<object>.Valid(new
                {
                    Message = "Baby details updated succesfully",
                    deliveredBabyInfo.Id
                }));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Result<object>.Invalid("An error occured while updating baby details"));
            }

        }
    }
}
