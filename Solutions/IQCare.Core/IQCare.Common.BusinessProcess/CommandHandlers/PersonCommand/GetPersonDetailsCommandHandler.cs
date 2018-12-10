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
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class GetPersonDetailsCommandHandler : IRequestHandler<GetPersonDetailsCommand, Result<PatientLookupView>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPersonDetailsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PatientLookupView>> Handle(GetPersonDetailsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("exec pr_OpenDecryptedSession;");
                    sql.Append($"SELECT TOP 1 * FROM Api_PatientsView WHERE PersonId = {request.PersonId};");
                    sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                    var result = await _unitOfWork.Repository<PatientLookupView>().FromSql(sql.ToString());
                    result.ForEach(item => item.CalculateYourAge());

                    return Result<PatientLookupView>.Valid(result.FirstOrDefault());
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<PatientLookupView>.Invalid(e.Message);
                }
            }
        }
    }
}