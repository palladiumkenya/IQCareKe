using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class DeactivateDrugAdministrationCommandHandler : IRequestHandler<DeactivateDrugAdministrationCommand,Result<object>>
    {
        private readonly IMaternityUnitOfWork _maternityUnitOfWork;
        public DeactivateDrugAdministrationCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }
        public Task<Result<object>> Handle(DeactivateDrugAdministrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var administeredDrug = _maternityUnitOfWork.Repository<MaternalDrugAdministration>()
                      .Get(x => x.Id == request.Id).SingleOrDefault();

                if (administeredDrug == null)
                    return Task.FromResult(Result<object>.Invalid("Drug administration details not found"));

                administeredDrug.DeactivateDrugAdministration();
                _maternityUnitOfWork.Repository<MaternalDrugAdministration>().Update(administeredDrug);
                _maternityUnitOfWork.Save();

                return Task.FromResult(Result<object>.Valid(new { Message = "Drug administration details deactivated successfully" }));
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occured while deactivating drug administration details with Id {request.Id}");
                return Task.FromResult(Result<object>.Invalid(ex.Message));
            }
        }
    }
}
