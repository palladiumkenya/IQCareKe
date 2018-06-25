using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using IQCare.Records.BusinessProcess.Command;
using System.Threading.Tasks;
using System.Threading;

namespace IQCare.Records.BusinessProcess.CommandHandlers.Lookup
{
    public  class SearchPersonCommandHandler:IRequestHandler<SearchPersonCommand,Result<SearchPersonResponse>>
    {
        List<PersonIdentifierView> list = new List<PersonIdentifierView>();
        
        private readonly ICommonUnitOfWork _unitOfWork;
        public SearchPersonCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        public async Task<Result<SearchPersonResponse>> Handle(SearchPersonCommand request,CancellationToken cancellationToken)
        {


            try
            {

                StringBuilder sql = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(request.firstName) || !string.IsNullOrWhiteSpace(request.identificationNumber) ||
                    !string.IsNullOrWhiteSpace(request.middleName) || !string.IsNullOrWhiteSpace(request.lastName))
                { 
                    sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append("Select top 10 * from PersonIdentifierView where (DeleteFlag=0 or DeleteFlag is null) ");
                }
                
               if(request.NotClient == true)
                {
                    sql.Append($" AND Patientid is null");
                }
                if (request.NotClient == false)
                {
                    sql.Append($"AND Patientid is not null");
                }
                if (!string.IsNullOrWhiteSpace(request.EnrollmentNumber))
                    sql.Append($" AND EnrollmentNumber like \'%{request.EnrollmentNumber}%\'");
              if (!string.IsNullOrWhiteSpace(request.identificationNumber))
                        sql.Append($" AND PersonIdentifierValue like \'%{request.identificationNumber}%\'");
                if (!string.IsNullOrWhiteSpace(request.identificationNumber))
                 sql.Append($" AND PatientIdentifierValue like \'%{request.identificationNumber}%\'");
                if(!string.IsNullOrWhiteSpace(request.firstName))
                sql.Append($" AND FirstName like \'%{request.firstName}%\'");
                if (!string.IsNullOrWhiteSpace(request.middleName))
                    sql.Append($" AND MiddleName like \'%{request.middleName}%\'");
                if (!string.IsNullOrWhiteSpace(request.lastName))
                    sql.Append($" AND LastName like \'%{request.lastName}%\';");
                sql.Append(";exec [dbo].[pr_CloseDecryptedSession];");
                var result = await _unitOfWork.Repository<PersonIdentifierView>().FromSql(sql.ToString());
                
                list = result;


                _unitOfWork.Dispose();


                return Result<SearchPersonResponse>.Valid(new SearchPersonResponse()
                {

                   PersonSearch = list,

                });
               
            }
            catch (Exception ex)
            {
                return Result<SearchPersonResponse>.Invalid(ex.Message);
            }

        
        }


    }
}
