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
    public class GetPatientDiagnosisInfoQueryHandler : IRequestHandler<GetPatientDiagnosisInfo, Result<List<PatientDiagnosisViewModel>>>
    {
        private readonly IMaternityUnitOfWork _maternityUnitOfWork;
        private readonly IMapper _mapper;
       private readonly ILogger _logger = Log.ForContext<GetPatientDiagnosisInfoQueryHandler>();

        public GetPatientDiagnosisInfoQueryHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }
        public Task<Result<List<PatientDiagnosisViewModel>>> Handle(GetPatientDiagnosisInfo request, CancellationToken cancellationToken)
        {
            try
            {
                var patientDiagnosis = request.PatientId.HasValue
                    ? _maternityUnitOfWork.Repository<PatientDiagnosis>().Get(x => x.PatientId == request.PatientId)
                    : _maternityUnitOfWork.Repository<PatientDiagnosis>().Get(x => x.PatientMasterVisitId == request.PatientMasterVisitId);

                var diagnosisViewModel = _mapper.Map<List<PatientDiagnosisViewModel>>(patientDiagnosis.AsEnumerable());


                return Task.FromResult(Result<List<PatientDiagnosisViewModel>>.Valid(diagnosisViewModel));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"An error occured while getting patient diagnosis info for patientId{ request.PatientId}");
                return Task.FromResult(Result<List<PatientDiagnosisViewModel>>.Invalid(ex.Message));
            }
            
        }
    }
}
