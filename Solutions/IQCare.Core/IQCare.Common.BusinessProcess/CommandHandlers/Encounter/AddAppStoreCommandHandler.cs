using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class AddAppStoreCommandHandler : IRequestHandler<AddAppStoreCommand, Result<AddAppStoreResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddAppStoreCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddAppStoreResponse>> Handle(AddAppStoreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    if (request.PersonId.HasValue && request.PatientId.HasValue && !request.PatientMasterVisitId.HasValue)
                    {
                        var appStateList = await _unitOfWork.Repository<AppStateStore>().Get(x =>
                            x.PersonId == request.PersonId.Value && x.PatientId == request.PatientId.Value &&
                            x.AppStateId == request.AppStateId).ToListAsync();

                        if (appStateList.Count > 0)
                        {
                            return Result<AddAppStoreResponse>.Valid(new AddAppStoreResponse()
                            {
                                IsSavedSuccessfully = true
                            });
                        }
                    }

                    AppStateStore appStateStore = new AppStateStore();
                    appStateStore.PersonId = request.PersonId;
                    appStateStore.PatientId = request.PatientId;
                    appStateStore.PatientMasterVisitId = request.PatientMasterVisitId;
                    appStateStore.EncounterId = request.EncounterId;
                    appStateStore.AppStateId = request.AppStateId;
                    appStateStore.StatusDate = DateTime.Now;
                    appStateStore.DeleteFlag = false;

                    await _unitOfWork.Repository<AppStateStore>().AddAsync(appStateStore);
                    await _unitOfWork.SaveAsync();

                    if (!string.IsNullOrWhiteSpace(request.AppStateObject))
                    {
                        await _unitOfWork.Repository<AppStateStoreObjects>().AddAsync(new AppStateStoreObjects()
                        {
                            AppStateStoreId = appStateStore.Id,
                            AppStateObject = request.AppStateObject
                            
                        });
                        await _unitOfWork.SaveAsync();
                    }

                    _unitOfWork.Dispose();

                    return Result<AddAppStoreResponse>.Valid(new AddAppStoreResponse()
                    {
                        IsSavedSuccessfully = true
                    });
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return Result<AddAppStoreResponse>.Invalid(e.Message);
            }
        }
    }
}