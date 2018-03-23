using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Partners;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Partners
{
    public class GetPartnersCommandHandler : IRequestHandler<GetPartnersCommand, Result<List<PartnersView>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPartnersCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<List<PartnersView>>> Handle(GetPartnersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string str = string.Join(",",request.RelationshipTypes.Select(i => "'" + i + "'"));

                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession; ");
                sql.Append($"SELECT [RowID],[PersonId],[PatientId],[FirstName],[MidName],[LastName],[DateOfBirth],[Sex],[Gender],[RelationshipTypeId],[RelationshipType] FROM HTS_PartnersView WHERE [PatientId] = {request.PatientId} AND RelationshipType IN ({ str });");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                var results = await _unitOfWork.Repository<PartnersView>().FromSql(sql.ToString());

                _unitOfWork.Dispose();

                return Result<List<PartnersView>>.Valid(results);
            }
            catch (Exception e)
            {
                return Result<List<PartnersView>>.Invalid(e.Message);
            }
        }
    }
}