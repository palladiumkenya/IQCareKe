using AutoMapper;
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
    public class GetDeliveredBabyBirthInfoQueryHandler : IRequestHandler<GetDeliveredBabyBirthInfoQuery, List<DeliveredBabyBirthInfoViewModel>>
    {
        readonly IMaternityUnitOfWork _maternityUnitOfWork;
        readonly IMapper _mapper;
        ILogger logger = Log.ForContext<GetDeliveredBabyBirthInfoQueryHandler>();

        public GetDeliveredBabyBirthInfoQueryHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }

        public Task<List<DeliveredBabyBirthInfoViewModel>> Handle(GetDeliveredBabyBirthInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var deliveredBabyBirthInfo = _maternityUnitOfWork.Repository<DeliveredBabyBirthInfoView>()
                        .Get(x => x.PatientDeliveryInformationId == request.PatientDeliveryInformationId).AsEnumerable();

                var birthInfoModel = _mapper.Map<List<DeliveredBabyBirthInfoViewModel>>(deliveredBabyBirthInfo);

                return Task.FromResult(birthInfoModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occured while fetching baby birth details for Delivery Id {request.PatientDeliveryInformationId}");
                return Task.FromResult(new List<DeliveredBabyBirthInfoViewModel>());
            }
        }
    }
}
