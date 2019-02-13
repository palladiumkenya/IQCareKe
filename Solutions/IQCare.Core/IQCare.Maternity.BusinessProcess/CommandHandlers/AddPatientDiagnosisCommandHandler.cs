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
    public class AddPatientDiagnosisCommandHandler : IRequestHandler<AddPatientDiagnosisCommand, Result<AddPatientDiagnosisResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<AddPatientDiagnosisCommandHandler>();

        public AddPatientDiagnosisCommandHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<AddPatientDiagnosisResponse>> Handle(AddPatientDiagnosisCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patientDiagnosis = _mapper.Map<PatientDiagnosis>(request);
                await _maternityUnitOfWork.Repository<PatientDiagnosis>().AddAsync(patientDiagnosis);
                await _maternityUnitOfWork.SaveAsync();

                return Result<AddPatientDiagnosisResponse>.Valid(new AddPatientDiagnosisResponse
                {
                    DiagnosisId = patientDiagnosis.Id
                });
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occured while adding patient diagnosis for patientId {request.PatientId}");
                return Result<AddPatientDiagnosisResponse>.Invalid(ex.Message);
            }
        }
    }
}
