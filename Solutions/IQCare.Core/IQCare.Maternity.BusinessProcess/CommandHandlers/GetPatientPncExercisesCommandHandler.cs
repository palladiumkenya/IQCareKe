using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.Core.Domain.PNC;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class GetPatientPncExercisesCommandHandler : IRequestHandler<GetPatientPncExercisesCommand, Result<List<PatientPncExercises>>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;

        public GetPatientPncExercisesCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }

        public async Task<Result<List<PatientPncExercises>>> Handle(GetPatientPncExercisesCommand request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    List<PatientPncExercises> result = await _maternityUnitOfWork.Repository<PatientPncExercises>().Get(x => x.PatientId == request.PatientId && x.PatientMasterVisitId == request.PatientMasterVisitId).ToListAsync();
                    return Result<List<PatientPncExercises>>.Valid(result);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"An error occured while fetching pnc exercises for PatientId: {request.PatientId} and patientmastervisitid: {request.PatientMasterVisitId}");
                    return Result<List<PatientPncExercises>>.Invalid(ex.Message);
                }
            }
        }
    }
}
