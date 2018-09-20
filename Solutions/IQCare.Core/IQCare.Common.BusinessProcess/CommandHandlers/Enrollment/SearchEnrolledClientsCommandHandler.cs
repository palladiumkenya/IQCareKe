using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Enrollment;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Enrollment
{
    public class SearchEnrolledClientsCommandHandler : IRequestHandler<SearchEnrolledClientsCommand, Result<List<PatientLookupView>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public SearchEnrolledClientsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<PatientLookupView>>> Handle(SearchEnrolledClientsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession; ");
                sql.Append("SELECT TOP 10 * FROM Api_PatientsView WHERE (DeleteFlag = 0 AND (ServiceAreaId = 2 OR ServiceAreaId IS NULL)) ");
                if(!string.IsNullOrWhiteSpace(request.identificationNumber))
                    sql.Append($" AND IdentifierValue like \'%{request.identificationNumber}%\'");
                if (!string.IsNullOrWhiteSpace(request.firstName))
                    sql.Append($" AND FirstName like \'%{request.firstName}%\'");
                if (!string.IsNullOrWhiteSpace(request.middleName))
                    sql.Append($" AND MidName like \'%{request.middleName}%\'");
                if (!string.IsNullOrWhiteSpace(request.lastName))
                    sql.Append($" AND LastName like \'%{request.lastName}%\';");
                sql.Append(";exec [dbo].[pr_CloseDecryptedSession];");


                var result = await _unitOfWork.Repository<PatientLookupView>().FromSql(sql.ToString());
                result.ForEach(item =>
                {
                    item.CalculateYourAge();
                    item.CheckIsHtsEnrolled();
                });

                _unitOfWork.Dispose();

                return Result<List<PatientLookupView>>.Valid(result);
            }
            catch (Exception ex)
            {
                return Result<List<PatientLookupView>>.Invalid(ex.Message);
            }
        }
    }
}