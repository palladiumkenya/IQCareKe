using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
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

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class AddMaternalDrugAdministrationCommandHandler : IRequestHandler<AddMaternalDrugAdministrationCommand, Result<AddMaternalDrugAdministrationResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        ILogger logger = Log.ForContext<AddMaternalDrugAdministrationCommandHandler>();

        public AddMaternalDrugAdministrationCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }
        public async Task<Result<AddMaternalDrugAdministrationResponse>> Handle(AddMaternalDrugAdministrationCommand request,
            CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    if (request.AdministredDrugs == null)
                        return Result<AddMaternalDrugAdministrationResponse>.Invalid("Administered drugs details not found");

                    var administredDrugs = request.AdministredDrugs.Select(x => new MaternalDrugAdministration(request.PatientId, request.PatientMasterVisitId, x.Id, x.Value, x.Description, request.CreatedBy)).ToList();

                    await _maternityUnitOfWork.Repository<MaternalDrugAdministration>().AddRangeAsync(administredDrugs);
                    await _maternityUnitOfWork.SaveAsync();

                    return Result<AddMaternalDrugAdministrationResponse>
                        .Valid(new AddMaternalDrugAdministrationResponse { PatientMasterVisitId = request.PatientMasterVisitId });
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"An error occured while adding drug admininstration details for PatientId {request.PatientId}");
                    return Result<AddMaternalDrugAdministrationResponse>.Invalid(ex.Message);
                }
            }
            

        }
    }
}
