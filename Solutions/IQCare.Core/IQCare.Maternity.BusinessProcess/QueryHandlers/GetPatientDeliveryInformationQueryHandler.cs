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
    public class GetPatientDeliveryInformationQueryHandler : IRequestHandler<GetPatientDeliveryInformationQuery, Result<List<PatientDeliveryInfomationViewModel>>>
    {
        readonly IMaternityUnitOfWork _maternityUnitOfWork;
        readonly IMapper _mapper;
        ILogger logger = Log.ForContext<GetPatientDeliveryInformationQueryHandler>();

        public GetPatientDeliveryInformationQueryHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }

        public Task<Result<List<PatientDeliveryInfomationViewModel>>> Handle(GetPatientDeliveryInformationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var patientDeliveryInfoView = _maternityUnitOfWork.Repository<PatientDeliveryInformationView>()
                        .Get(x => x.ProfileId == request.ProfileId).AsEnumerable();

                var patientDeliveryInfoModel = _mapper.Map<List<PatientDeliveryInfomationViewModel>>(patientDeliveryInfoView);

                return Task.FromResult(Result<List<PatientDeliveryInfomationViewModel>>.Valid(patientDeliveryInfoModel));
            }
            catch (Exception ex)
            {
                logger.Error($"An error occured while fetching patient delivery information for ProfileId {request.ProfileId}", ex);
                return Task.FromResult(Result<List<PatientDeliveryInfomationViewModel>>.Invalid(ex.Message));
            }
        }
    }
}
