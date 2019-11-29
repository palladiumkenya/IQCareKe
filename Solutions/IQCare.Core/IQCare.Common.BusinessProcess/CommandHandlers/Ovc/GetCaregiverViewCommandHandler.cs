using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Ovc;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Ovc
{
   public  class GetCaregiverViewCommandHandler : IRequestHandler<GetCaregiverViewCommmand, Result<List<CaregiverView>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetCaregiverViewCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<List<CaregiverView>>> Handle(GetCaregiverViewCommmand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                 

                    StringBuilder sql = new StringBuilder();
                    sql.Append("exec pr_OpenDecryptedSession; ");
                    sql.Append($"SELECT [RowID],[PersonId],[PatientId],[FirstName],[MidName],[LastName],[DateOfBirth],[Sex],[Gender],[RelationshipTypeId],[RelationshipType] FROM OVC_CaregiverView WHERE [PatientId] = {request.PatientId} ");
                    sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                    var results = await _unitOfWork.Repository<CaregiverView>().FromSql(sql.ToString());

                    _unitOfWork.Dispose();

                    return Result<List<CaregiverView>>.Valid(results);
                }
            }
            catch (Exception e)
            {
                return Result<List<CaregiverView>>.Invalid(e.Message);
            }
        }
    }
}
