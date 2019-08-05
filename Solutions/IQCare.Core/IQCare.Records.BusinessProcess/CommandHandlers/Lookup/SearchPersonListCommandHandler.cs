using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IQCare.Records.BusinessProcess.Command.Lookup;
using MediatR;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using Serilog;

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

                var firstName = string.IsNullOrWhiteSpace(request.firstName) ? "" : request.firstName.Replace("'", "''");
                var middleName = string.IsNullOrWhiteSpace(request.middleName) ? "" : request.middleName.Replace("'", "''");
                var lastName = string.IsNullOrWhiteSpace(request.lastName) ? "" : request.lastName.Replace("'", "''");

                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append("Select  top 100.* from PersonListView where (DeleteFlag=0 or DeleteFlag is null) ");

                if (!string.IsNullOrWhiteSpace(firstName))
                    sql.Append($" AND FirstName like \'%{firstName.Trim()}%\'");

                if (!string.IsNullOrWhiteSpace(middleName))
                    sql.Append($" AND MiddleName like \'%{middleName.Trim()}%\'");

                if (!string.IsNullOrWhiteSpace(lastName))
                    sql.Append($" AND LastName like \'%{lastName.Trim()}%\'");

                if (!string.IsNullOrWhiteSpace(request.identificationNumber))
                    sql.Append($" AND IdentifierValue like \'%{request.identificationNumber.Trim()}%\'");

                if (!string.IsNullOrWhiteSpace(request.MobileNumber))
                    sql.Append($" AND MobileNumber like \'%{request.MobileNumber.Trim()}%\'");

                if (request.BirthDate.HasValue)
                    sql.Append($" AND DateOfBirth = \'{request.BirthDate}\'");

                if (request.Sex.HasValue)
                    sql.Append($" AND Sex = \'{request.Sex}\'");

                sql.Append(";exec [dbo].[pr_CloseDecryptedSession];");
                var result = await _unitOfWork.Repository<PersonListView>().FromSql(sql.ToString());
                result.ForEach(item => item.CalculateYourAge());
                _unitOfWork.Dispose();

                if (request.Age.HasValue)
                {
                    result = result.FindAll(x => x.AgeNumber == request.Age.Value);
                }

                return Result<SearchPersonListResponse>.Valid(new SearchPersonListResponse()
                {
                    PersonSearch = result
                });
            }
            catch (Exception e)
            {
                Log.Error($"Error searching persons, exception message: {e.Message}, innerexception: {e.InnerException}");
                return Result<SearchPersonListResponse>.Invalid($"Error searching persons, exception message: {e.Message}");
            }
        }
    }
}
