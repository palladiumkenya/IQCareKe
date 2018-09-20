using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Records.BusinessProcess.Command.Lookup;
using MediatR;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Records.BusinessProcess.CommandHandlers.Lookup
{
   public class SearchPersonListCommandHandler:IRequestHandler<SearchPersonListCommand,Result<SearchPersonListResponse>>
    {
        List<PersonListView> list = new List<PersonListView>();

        private readonly ICommonUnitOfWork _unitOfWork;

        public SearchPersonListCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<SearchPersonListResponse>> Handle(SearchPersonListCommand request, CancellationToken cancellationToken)
        {


            try
            {

                StringBuilder sql = new StringBuilder();

                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append("Select  top 100.* from PersonListView where (DeleteFlag=0 or DeleteFlag is null) ");

                if (!string.IsNullOrWhiteSpace(request.firstName))
                    sql.Append($" AND FirstName like \'%{request.firstName}%\'");

                if (!string.IsNullOrWhiteSpace(request.middleName))
                    sql.Append($" AND MiddleName like \'%{request.middleName}%\'");

                if (!string.IsNullOrWhiteSpace(request.lastName))
                    sql.Append($" AND LastName like \'%{request.lastName}%\'");

                if (!string.IsNullOrWhiteSpace(request.identificationNumber))
                    sql.Append($" AND IdentifierValue like \'%{request.identificationNumber}%\'");
                if (!string.IsNullOrWhiteSpace(request.MobileNumber))
                    sql.Append($" AND MobileNumber like \'%{request.MobileNumber}%\'");

                sql.Append(";exec [dbo].[pr_CloseDecryptedSession];");
                var result = await _unitOfWork.Repository<PersonListView>().FromSql(sql.ToString());
                _unitOfWork.Dispose();

                return Result<SearchPersonListResponse>.Valid(new SearchPersonListResponse()
                {
                    PersonSearch = result
                });
            }
            catch (Exception e)
            {
                return Result<SearchPersonListResponse>.Invalid(e.Message);
               

            }
        }
    }
}
