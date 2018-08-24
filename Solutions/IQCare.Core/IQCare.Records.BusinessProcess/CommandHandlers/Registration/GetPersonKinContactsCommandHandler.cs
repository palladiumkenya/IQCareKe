using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using IQCare.Records.BusinessProcess.Command.Registration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonKinContactsView = IQCare.Common.Core.Models.PersonKinContactsView;

namespace IQCare.Records.BusinessProcess.CommandHandlers.Registration
{
    public class GetPersonKinContactsCommandHandler : IRequestHandler<GetPersonKinContactsCommand, Result<List<PersonKinContactsView>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPersonKinContactsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<PersonKinContactsView>>> Handle(GetPersonKinContactsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("exec pr_OpenDecryptedSession;");
                    sql.Append($"SELECT * FROM [dbo].[PersonKinContactsView] WHERE DeleteFlag = 0 AND PersonId = {request.PersonId};");
                    sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                    var result = await _unitOfWork.Repository<PersonKinContactsView>().FromSql(sql.ToString());
                    return Result<List<PersonKinContactsView>>.Valid(result);
                }
                catch (Exception e)
                {
                    return Result<List<PersonKinContactsView>>.Invalid(e.Message);
                }
            }
        }
    }
}