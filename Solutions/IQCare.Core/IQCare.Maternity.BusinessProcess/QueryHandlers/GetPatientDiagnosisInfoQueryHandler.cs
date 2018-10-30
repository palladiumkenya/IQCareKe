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
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<GetPatientDiagnosisInfoQueryHandler>();

        public GetPatientDiagnosisInfoQueryHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }
        public Task<Result<List<PatientDiagnosisViewModel>>> Handle(GetPatientDiagnosisInfo request, CancellationToken cancellationToken)
        {
            try
            {
                var patientDiagnosis = _maternityUnitOfWork.Repository<PatientDiagnosis>().Get(x => x.PatientId == request.PatientId)
                        .AsEnumerable();
                var diagnosisViewModel = _mapper.Map<List<PatientDiagnosisViewModel>>(patientDiagnosis);


                return Task.FromResult(Result<List<PatientDiagnosisViewModel>>.Valid(diagnosisViewModel));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occured while getting patient diagnosis info for patientId{ request.PatientId}");
                return Task.FromResult(Result<List<PatientDiagnosisViewModel>>.Invalid(ex.Message));
            }
            
        }
    }
}
