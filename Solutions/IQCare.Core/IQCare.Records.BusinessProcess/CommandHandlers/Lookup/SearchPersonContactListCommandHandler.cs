using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using IQCare.Records.BusinessProcess.Command;
using System.Threading.Tasks;
using System.Threading;
using IQCare.Library;

namespace IQCare.Records.BusinessProcess.CommandHandlers.Lookup
{
    public class SearchPersonContactListCommandHandler : IRequestHandler<SearchPersonContactListCommand, Result<SearchResponse>>
    {
        List<ContactsListView> list = new List<ContactsListView>();
        private readonly ICommonUnitOfWork _unitOfWork;

        public SearchPersonContactListCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        public async Task<Result<SearchResponse>> Handle(SearchPersonContactListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                StringBuilder sql = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(request.firstName) || !string.IsNullOrWhiteSpace(request.identificationNumber) ||
                    !string.IsNullOrWhiteSpace(request.middleName) || !string.IsNullOrWhiteSpace(request.lastName))
                {
                    sql.Append("exec pr_OpenDecryptedSession;");
                    sql.Append("Select top 10 * from ContactsListView where DeleteFlag is null or DeleteFlag=0");
                }
                else
                {
                    sql.Append("exec pr_OpenDecryptedSession;");
                    sql.Append("Select top 10 * from ContactsListView where DeleteFlag is null or DeleteFlag=0 ");

                }

                if (!string.IsNullOrWhiteSpace(request.EnrollmentNumber))
                    sql.Append($"AND EnrollmentNumber like \'%{request.EnrollmentNumber}%\'");
                if (!string.IsNullOrWhiteSpace(request.identificationNumber))
                    sql.Append($" AND PersonIdentificationNumber like \'%{request.identificationNumber}%\'");
               
                if (!string.IsNullOrWhiteSpace(request.firstName))
                    sql.Append($" AND FirstName like \'%{request.firstName}%\'");
                if (!string.IsNullOrWhiteSpace(request.middleName))
                    sql.Append($" AND MiddleName like \'%{request.middleName}%\'");
                if (!string.IsNullOrWhiteSpace(request.lastName))
                    sql.Append($" AND LastName like \'%{request.lastName}%\';");
                sql.Append(";exec [dbo].[pr_CloseDecryptedSession];");
                var result = await _unitOfWork.Repository<ContactsListView>().FromSql(sql.ToString());

                list = result;


                _unitOfWork.Dispose();


                return Result<SearchResponse>.Valid(new SearchResponse()
                {

                    PersonSearch = list,

                });

            }
            catch (Exception ex)
            {
                return Result<SearchResponse>.Invalid(ex.Message);
            }

        }
    }
}

