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
    public class GetPatientDrugAdministrationInfoQueryHandler : IRequestHandler<GetPatientDrugAdministrationInfoQuery, Result<List<PatientDrugAdministrationViewModel>>>
    {
        private readonly ILogger _logger = Log.ForContext<GetPatientDrugAdministrationInfoQueryHandler>();
        private readonly  IMaternityUnitOfWork _maternityUnitOfWork;
        private  readonly IMapper _mapper;
        public GetPatientDrugAdministrationInfoQueryHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }

        public Task<Result<List<PatientDrugAdministrationViewModel>>> Handle(GetPatientDrugAdministrationInfoQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var patientDiagnosisInfo = _maternityUnitOfWork.Repository<PatientDrugAdministrationView>()
                       .Get(x => x.PatientId == request.PatientId).AsEnumerable();

                var diagnosisInfoModel = _mapper.Map<List<PatientDrugAdministrationViewModel>>(patientDiagnosisInfo);

                return Task.FromResult(Result<List<PatientDrugAdministrationViewModel>>.Valid(diagnosisInfoModel));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"An error occured while getting patient drug administration for Id {request.PatientId}");
                return Task.FromResult(Result<List<PatientDrugAdministrationViewModel>>.Invalid(ex.Message));
            }
        }
    }
}
