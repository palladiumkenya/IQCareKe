using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class GetPersonContactViewCommandHandler : IRequestHandler<GetPersonContactViewCommand, Result<PersonContactView>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPersonContactViewCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PersonContactView>> Handle(GetPersonContactViewCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("exec pr_OpenDecryptedSession;");
                    sql.Append($"SELECT * FROM PersonContactView WHERE PersonId = {request.personId} AND DeleteFlag = 0; ");
                    sql.Append("exec [dbo].[pr_CloseDecryptedSession];");
                    var result = await _unitOfWork.Repository<PersonContactView>().FromSql(sql.ToString());

                    return Result<PersonContactView>.Valid(result.FirstOrDefault()) ;
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<PersonContactView>.Invalid(e.Message);
                }
            }
        }
    }
}
