using MediatR;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Infrastructure.UnitOfWork;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class AddMaternalDrugAdministrationCommandHandler : IRequestHandler<AddPatientDrugAdministrationCommand, Result<AddPatientDrugAdministrationResponse>>
    {
        
        private readonly IMaternityUnitOfWork _maternityUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<AddMaternalDrugAdministrationCommandHandler>();

        public AddMaternalDrugAdministrationCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }
        public async Task<Result<AddPatientDrugAdministrationResponse>> Handle(AddPatientDrugAdministrationCommand request,
            CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    if (request.AdministeredDrugs == null)
                        return Result<AddPatientDrugAdministrationResponse>.Invalid("Administered drugs details not found");

                    var administeredDrugs = request.AdministeredDrugs.Select(x => new MaternalDrugAdministration(request.PatientId, request.PatientMasterVisitId, x.Id, x.Value, x.Description, request.CreatedBy)).ToList();

                    await _maternityUnitOfWork.Repository<MaternalDrugAdministration>().AddRangeAsync(administeredDrugs);
                    await _maternityUnitOfWork.SaveAsync();

                    return Result<AddPatientDrugAdministrationResponse>
                        .Valid(new AddPatientDrugAdministrationResponse { PatientMasterVisitId = request.PatientMasterVisitId });
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"An error occured while adding drug administration details for PatientId {request.PatientId}");
                    return Result<AddPatientDrugAdministrationResponse>.Invalid(ex.Message);
                }
            }
            

        }
    }
}
