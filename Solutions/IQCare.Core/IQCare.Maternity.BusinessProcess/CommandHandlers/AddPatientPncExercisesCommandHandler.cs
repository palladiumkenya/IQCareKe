using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.Core.Domain.PNC;
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
    public class AddPatientPncExercisesCommandHandler : IRequestHandler<AddPatientPncExercisesCommand, Result<AddPatientPncExercisesResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;

        public AddPatientPncExercisesCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }

        public async Task<Result<AddPatientPncExercisesResponse>> Handle(AddPatientPncExercisesCommand request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    int patientPncExercisesId = 0;
                    var pncExercises = await _maternityUnitOfWork.Repository<PatientPncExercises>().FindByIdAsync(request.Id);
                    if(pncExercises != null)
                    {
                        pncExercises.PncExercisesDone = request.PncExercisesDoneId;

                        _maternityUnitOfWork.Repository<PatientPncExercises>().Update(pncExercises);
                        await _maternityUnitOfWork.SaveAsync();

                        patientPncExercisesId = pncExercises.Id;
                    }
                    else
                    {
                        PatientPncExercises patientPncExercises = new PatientPncExercises()
                        {
                            PatientId = request.PatientId,
                            PatientMasterVisitId = request.PatientMasterVisitId,
                            PncExercisesDone = request.PncExercisesDoneId,
                            CreateDate = DateTime.Now,
                            CreatedBy = request.UserId
                        };

                        await _maternityUnitOfWork.Repository<PatientPncExercises>().AddAsync(patientPncExercises);
                        await _maternityUnitOfWork.SaveAsync();

                        patientPncExercisesId = patientPncExercises.Id;
                    }

                    return Result<AddPatientPncExercisesResponse>.Valid(new AddPatientPncExercisesResponse {
                        PncExercisesId = patientPncExercisesId
                    });
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"An error occured while pnc exercises for PatientId:   {request.PatientId}");
                    return Result<AddPatientPncExercisesResponse>.Invalid(ex.Message);
                }
            }
        }
    }
}
