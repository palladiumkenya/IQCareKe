using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers
{
    public class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, Result<RegisterClientResponse>>
    {

        private readonly ICommonUnitOfWork _unitOfWork;
        public RegisterClientCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<RegisterClientResponse>> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var sql =
                    "exec pr_OpenDecryptedSession;" +
                    "Insert Into Person(FirstName, MidName,LastName,Sex,DateOfBirth,DobPrecision,Active,DeleteFlag,CreateDate,CreatedBy)" +
                    $"Values(ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{request.Person.FirstName}'), ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{request.Person.MiddleName}')," +
                    $"ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{request.Person.LastName}'), {request.Person.Sex}, '{request.Person.DateOfBirth}', 1," +
                    "1,0,GETDATE(),1);" +
                    "SELECT [Id] , CAST(DECRYPTBYKEY(FirstName) AS VARCHAR(50)) [FirstName] ,CAST(DECRYPTBYKEY(MidName) AS VARCHAR(50)) MidName" +
                    ",CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50)) [LastName] ,[Sex] ,[Active] ,[DeleteFlag] ,[CreateDate] " +
                    ",[CreatedBy] ,[AuditData] ,[DateOfBirth] ,[DobPrecision] FROM [dbo].[Person] WHERE Id = SCOPE_IDENTITY();" +
                    "exec [dbo].[pr_CloseDecryptedSession];";

                var personInsert = await _unitOfWork.Repository<Person>().FromSql(sql);

                _unitOfWork.Dispose();

                return Result<RegisterClientResponse>.Valid(new RegisterClientResponse {PersonId = personInsert[0].Id});

            }
            catch (Exception e)
            {
                return Result<RegisterClientResponse>.Invalid(e.Message);
            }
        }
    }
}