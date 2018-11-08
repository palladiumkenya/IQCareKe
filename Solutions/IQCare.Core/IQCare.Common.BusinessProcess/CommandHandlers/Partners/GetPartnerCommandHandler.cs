using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Relationship;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Partners
{
    public class GetPartnerCommandHandler : IRequestHandler<GetPartnerCommand, Result<PartnersView>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPartnerCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PartnersView>> Handle(GetPartnerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("exec pr_OpenDecryptedSession; ");
                    sql.Append($"SELECT [RowID],[PersonId],[PatientId],[FirstName],[MidName],[LastName],[DateOfBirth],[Sex],[Gender],[RelationshipTypeId],[RelationshipType] FROM HTS_PartnersView WHERE [PersonId] = {request.PersonId};");
                    sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                    var results = await _unitOfWork.Repository<PartnersView>().FromSql(sql.ToString());

                    _unitOfWork.Dispose();

                    return Result<PartnersView>.Valid(results.FirstOrDefault());
                }
            }
            catch (Exception e)
            {
                return Result<PartnersView>.Invalid(e.Message);
            }
        }
    }
}