using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class GetAppStoreCommandHandler : IRequestHandler<GetAppStoreCommand, Result<GetAppStoreResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetAppStoreCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<GetAppStoreResponse>> Handle(GetAppStoreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    Expression<Func<AppStateStore, bool>> expressionFinal = x => x.PersonId > 0;

                    if (request.PatientId != null)
                    {
                        Expression<Func<AppStateStore, bool>> expressionPatientId = c => c.PatientId == request.PatientId;

                        expressionFinal = PredicateBuilder.And(expressionFinal, expressionPatientId);
                    }

                    if (request.PersonId != null)
                    {
                        Expression<Func<AppStateStore, bool>> expressionPersonId = c => c.PersonId == request.PersonId;

                        expressionFinal = PredicateBuilder.And(expressionFinal, expressionPersonId);
                    }

                    if (request.PatientMasterVisitId != null)
                    {
                        Expression<Func<AppStateStore, bool>> expressionPatientMasterVisitId = c => c.PatientMasterVisitId == request.PatientMasterVisitId || c.PatientMasterVisitId == null;

                        expressionFinal = PredicateBuilder.And(expressionFinal, expressionPatientMasterVisitId);
                    }

                    if (request.EncounterId != null)
                    {
                        Expression<Func<AppStateStore, bool>> expressionEncounterId = c => c.EncounterId == request.EncounterId || c.EncounterId == null;

                        expressionFinal = PredicateBuilder.And(expressionFinal, expressionEncounterId);
                    }

                    var result = await _unitOfWork.Repository<AppStateStore>().Get(expressionFinal).Include(b=>b.AppStateStoreObjects).ToListAsync();

                    _unitOfWork.Dispose();

                    return Result<GetAppStoreResponse>.Valid(new GetAppStoreResponse()
                    {
                        StateStore = result
                    });
                }
            }
            catch (Exception e)
            {
                return Result<GetAppStoreResponse>.Invalid(e.Message);
            }
        }
    }
}