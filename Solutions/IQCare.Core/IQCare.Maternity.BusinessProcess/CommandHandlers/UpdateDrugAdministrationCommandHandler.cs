using System;
using System.Linq;
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
    public class UpdateDrugAdministrationCommandHandler : IRequestHandler<UpdateDrugAdministrationCommand,Result<object>>
    {
        private readonly IMaternityUnitOfWork _maternityUnitOfWork;
        public UpdateDrugAdministrationCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }
        public Task<Result<object>> Handle(UpdateDrugAdministrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var drugAdministration = _maternityUnitOfWork.Repository<MaternalDrugAdministration>()
                       .Get(x => x.Id == request.Id).SingleOrDefault();

                if (drugAdministration == null)
                    return Task.FromResult(Result<object>.Invalid("Drug administration details not found"));
               
                drugAdministration.Update(request.DrugAdministered, request.Value, request.Description);
                _maternityUnitOfWork.Repository<MaternalDrugAdministration>().Update(drugAdministration);
                _maternityUnitOfWork.Save();

                return Task.FromResult(Result<object>.Valid(new { Message = "Drug administration details updated successfully" }));
            }
            catch (Exception ex)
            {
                var message = $"An error occured while updating Drug administration details with Id {request.Id}";
                Log.Error(ex, message);
                return Task.FromResult(Result<object>.Invalid(message));
            }
        }
    }
}
