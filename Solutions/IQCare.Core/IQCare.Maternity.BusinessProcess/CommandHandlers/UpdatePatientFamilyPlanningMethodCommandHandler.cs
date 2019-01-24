using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.Core.Domain.PNC;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class UpdatePatientFamilyPlanningMethodCommandHandler : IRequestHandler<UpdatePatientFamilyPlanningMethodCommand, Result<UpdatePatientFamilyPlanningMethodResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;

        public UpdatePatientFamilyPlanningMethodCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }

        public async Task<Result<UpdatePatientFamilyPlanningMethodResponse>> Handle(UpdatePatientFamilyPlanningMethodCommand request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    var familyPlanningMethod = await _maternityUnitOfWork.Repository<PatientFamilyPlanningMethod>()
                        .FindByIdAsync(request.Id);
                    if (familyPlanningMethod != null)
                    {
                        familyPlanningMethod.FPMethodId = request.FPMethodId;

                        _maternityUnitOfWork.Repository<PatientFamilyPlanningMethod>().Update(familyPlanningMethod);
                        await _maternityUnitOfWork.SaveAsync();
                    }
                    else
                    {
                        PatientFamilyPlanningMethod patientFamilyPlanningMethod =
                            new PatientFamilyPlanningMethod(request.PatientId, request.PatientFPId, request.FPMethodId,
                                request.UserId);

                        await _maternityUnitOfWork.Repository<PatientFamilyPlanningMethod>()
                            .AddAsync(patientFamilyPlanningMethod);
                        await _maternityUnitOfWork.SaveAsync();
                    }

                    return Result<UpdatePatientFamilyPlanningMethodResponse>.Valid(new UpdatePatientFamilyPlanningMethodResponse()
                    {
                        Message = $"Successfully updated patientfamilyplanningmethod with id: {request.Id}"
                    });
                }
                catch (Exception e)
                {
                    Log.Error($"Error updating family planning method with id: {request.Id}");
                    return Result<UpdatePatientFamilyPlanningMethodResponse>.Invalid($"Error updating family planning method with id: {request.Id}");
                }
            }
        }
    }
}