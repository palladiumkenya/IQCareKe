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
    public class UpdatePatientFamilyPlanningCommandHandler : IRequestHandler<UpdatePatientFamilyPlanningCommand, Result<UpdatePatientFamilyPlanningResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        public UpdatePatientFamilyPlanningCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }

        public async Task<Result<UpdatePatientFamilyPlanningResponse>> Handle(UpdatePatientFamilyPlanningCommand request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    var familyPlanning = await _maternityUnitOfWork.Repository<PatientFamilyPlanning>()
                        .FindByIdAsync(request.Id);
                    if (familyPlanning != null)
                    {

                        familyPlanning.FamilyPlanningStatusId = request.FamilyPlanningStatusId;
                        familyPlanning.ReasonNotOnFPId = request.ReasonNotOnFPId;

                        _maternityUnitOfWork.Repository<PatientFamilyPlanning>().Update(familyPlanning);
                        await _maternityUnitOfWork.SaveAsync();

                        return Result<UpdatePatientFamilyPlanningResponse>.Valid(new UpdatePatientFamilyPlanningResponse()
                        {
                            Message = "Successfully update familyplanning"
                        });
                    }
                    else
                    {
                        Log.Error($"Family Planning with id: {request.Id} could not be found");
                        return Result<UpdatePatientFamilyPlanningResponse>.Invalid($"Family Planning with id: {request.Id}  could not be found");
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Error updating PatientFamilyPlanning with id: {request.Id}");
                    return Result<UpdatePatientFamilyPlanningResponse>.Invalid($"Error updating PatientFamilyPlanning with id: {request.Id}");
                }
            }
        }
    }
}