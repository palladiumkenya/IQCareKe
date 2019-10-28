using IQCare.Common.BusinessProcess.Commands.Lookup;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Lookup
{
    public class GetDecodeByCodeIdCommandHandler : IRequestHandler<GetDecodeByCodeIdCommand, Result<GetDecodeByCodeIdResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;


        public GetDecodeByCodeIdCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<GetDecodeByCodeIdResponse>> Handle(GetDecodeByCodeIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var results = await _unitOfWork.Repository<Decode>().Get(x =>x.CodeID == request.CodeId && x.DeleteFlag == 0)
                    .ToListAsync();

                _unitOfWork.Dispose();

                return Result<GetDecodeByCodeIdResponse>.Valid(new GetDecodeByCodeIdResponse
                {
                    DecodeItems = results
                });
            }
            catch (Exception ex)
            {
                return Result<GetDecodeByCodeIdResponse>.Invalid(ex.Message);
            }
        }
    }
}
