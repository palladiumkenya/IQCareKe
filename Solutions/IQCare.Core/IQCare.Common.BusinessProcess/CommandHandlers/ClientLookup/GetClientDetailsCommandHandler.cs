using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.ClientLookup;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.ClientLookup
{
    public class GetClientDetailsCommandHandler : IRequestHandler<GetClientDetailsCommand, Result<GetClientDetailsResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetClientDetailsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<GetClientDetailsResponse>> Handle(GetClientDetailsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sql = "exec pr_OpenDecryptedSession;" +
                          $"SELECT * FROM Api_PatientsView WHERE ServiceAreaId = {request.ServiceAreaId}" +
                          $"AND PatientId = {request.PatientId};" +
                          "exec [dbo].[pr_CloseDecryptedSession];";

                

                var result = await _unitOfWork.Repository<PatientLookupView>().FromSql(sql).ToListAsync();

                result.ForEach(item=>item.CalculateYourAge());

                return Result<GetClientDetailsResponse>.Valid(new GetClientDetailsResponse
                {
                    PatientLookup = result
                });
            }
            catch (Exception ex)
            {
                return Result<GetClientDetailsResponse>.Invalid(ex.Message);
            }
        }
    }
}