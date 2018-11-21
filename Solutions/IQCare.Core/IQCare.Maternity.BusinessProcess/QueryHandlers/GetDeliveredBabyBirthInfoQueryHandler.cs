using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Queries.Maternity;
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

namespace IQCare.Maternity.BusinessProcess.QueryHandlers
{
    public class GetDeliveredBabyBirthInfoQueryHandler : IRequestHandler<GetDeliveredBabyBirthInfoQuery, Result<List<DeliveredBabyBirthInfoViewModel>>>
    {
        private readonly IMaternityUnitOfWork _maternityUnitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _Logger = Log.ForContext<GetDeliveredBabyBirthInfoQueryHandler>();
        
        public GetDeliveredBabyBirthInfoQueryHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }

        public Task<Result<List<DeliveredBabyBirthInfoViewModel>>> Handle(GetDeliveredBabyBirthInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var deliveredBabyBirthInfoQueryable = request.PatientMasterVisitId.HasValue
                    ? _maternityUnitOfWork.Repository<DeliveredBabyBirthInfoView>().Get(x =>x.PatientMasterVisitId == request.PatientMasterVisitId.Value)
                    : _maternityUnitOfWork.Repository<DeliveredBabyBirthInfoView>().Get(x => x.PatientDeliveryInformationId == request.PatientDeliveryInformationId);

                var deliveredBabyBirthInfo = deliveredBabyBirthInfoQueryable.Where(x => x.DeleteFlag == false).AsEnumerable();

                var birthInfoModel = _mapper.Map<List<DeliveredBabyBirthInfoViewModel>>(deliveredBabyBirthInfo);

                var deliveredBabyInfoIds = birthInfoModel.Select(x => x.Id).ToList();

                var apgarScores = _maternityUnitOfWork.Repository<DeliveredBabyApgarScoreView>()
                    .Get(x => deliveredBabyInfoIds.Contains(x.DeliveredBabyBirthInformationId));

                if (apgarScores.Any())
                {
                    birthInfoModel.ForEach(birth =>
                    {
                        var formatedApgarScores = apgarScores.Where(x => x.DeliveredBabyBirthInformationId == birth.Id)
                        .Select(x => x.FormatApgarScore()).ToArray();

                        birth.ApgarScores = string.Join(",", formatedApgarScores);
                    });
                }

                return Task.FromResult(Result<List<DeliveredBabyBirthInfoViewModel>>.Valid(birthInfoModel));
            }
            catch (Exception ex)
            {
                _Logger.Error(ex, $"An error occured while fetching baby birth details for Delivery Id {request.PatientDeliveryInformationId}");
                return Task.FromResult(Result<List<DeliveredBabyBirthInfoViewModel>>.Invalid(ex.Message));
            }
        }
    }
}
