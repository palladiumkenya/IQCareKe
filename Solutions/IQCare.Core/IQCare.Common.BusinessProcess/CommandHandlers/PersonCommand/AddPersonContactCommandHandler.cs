using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class AddPersonContactCommandHandler : IRequestHandler<AddPersonContactCommand, Result<AddPersonContactResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddPersonContactCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPersonContactResponse>> Handle(AddPersonContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("exec pr_OpenDecryptedSession;");
                    sql.Append("INSERT INTO PersonContact(PersonId,PhysicalAddress,MobileNumber,AlternativeNumber,EmailAddress,Active,DeleteFlag,CreateDate,CreatedBy, AuditData)");
                    sql.Append($"VALUES('{request.PersonId}', ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{request.PhysicalAddress}'), ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{request.MobileNumber}'), ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{request.AlternativeNumber}'), ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{request.EmailAddress}'), 1, 0, GETDATE(), '{request.UserId}', NULL);");
                    sql.Append("SELECT [Id], [PersonId], CAST(DECRYPTBYKEY([PhysicalAddress]) AS VARCHAR(50)) [PhysicalAddress], CAST(DECRYPTBYKEY([MobileNumber]) AS VARCHAR(50)) [MobileNumber], CAST(DECRYPTBYKEY([AlternativeNumber]) AS VARCHAR(50)) [AlternativeNumber], CAST(DECRYPTBYKEY([EmailAddress]) AS VARCHAR(50)) [EmailAddress], [Active], [DeleteFlag], [CreatedBy], [CreateDate], AuditData FROM [dbo].[PersonContact] WHERE Id = SCOPE_IDENTITY();");
                    sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                    var personContactInsert = await _unitOfWork.Repository<PersonContact>().FromSql(sql.ToString());

                    _unitOfWork.Dispose();

                    return Result<AddPersonContactResponse>.Valid(new AddPersonContactResponse()
                    {
                        PersonContactId = personContactInsert[0].Id
                    });
                }
            }
            catch (Exception e)
            {
                return Result<AddPersonContactResponse>.Invalid(e.Message);
            }
        }
    }
}