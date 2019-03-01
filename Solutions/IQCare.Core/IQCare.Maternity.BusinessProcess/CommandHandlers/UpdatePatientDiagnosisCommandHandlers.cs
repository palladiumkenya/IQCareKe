using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class UpdatePatientDiagnosisCommandHandlers : IRequestHandler<UpdatePatientDiagnosisCommand, Result<UpdatePatientDiagnosisResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;

        public UpdatePatientDiagnosisCommandHandlers(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UpdatePatientDiagnosisResponse>> Handle(UpdatePatientDiagnosisCommand request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    var patientDiagnosis = await _maternityUnitOfWork.Repository<PatientDiagnosis>().Get(x=>x.Id == request.DiagnosisId)
                        .SingleOrDefaultAsync();

                    if(patientDiagnosis==null)
                        return Result<UpdatePatientDiagnosisResponse>.Invalid("Patient diagnosis could not be found");

                    patientDiagnosis.Update(request.DiagnosisCommand.Diagnosis, request.DiagnosisCommand.ManagementPlan);
                    _maternityUnitOfWork.Repository<PatientDiagnosis>().Update(patientDiagnosis);
                    await _maternityUnitOfWork.SaveAsync();

                    return Result<UpdatePatientDiagnosisResponse>.Valid(new UpdatePatientDiagnosisResponse()
                    {
                        MessageResult = "Successfully updated patient diagnosis"
                    });
                }
                catch (Exception e)
                {
                    Log.Error("Error editing patient diagnosis " + e.Message + " " + e.InnerException);
                    return Result<UpdatePatientDiagnosisResponse>.Invalid(e.Message);
                }
            }
        }
    }
}