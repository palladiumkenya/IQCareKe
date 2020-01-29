using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class MergeDuplicateRecordsCommandHandler : IRequestHandler<MergeDuplicateRecordsCommand, Result<MergeDuplicateRecordsResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public MergeDuplicateRecordsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<MergeDuplicateRecordsResponse>> Handle(MergeDuplicateRecordsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("MergePatientData @preferredPersonId, @unPreferredPersonId, @userid");

                    var mergePreferredPersonId = new SqlParameter();
                    mergePreferredPersonId.SqlDbType = SqlDbType.Int;
                    mergePreferredPersonId.ParameterName = "@preferredPersonId";
                    mergePreferredPersonId.Value = request.preferredPersonId;

                    var mergeUnPreferredPersonId = new SqlParameter();
                    mergeUnPreferredPersonId.SqlDbType = SqlDbType.Int;
                    mergeUnPreferredPersonId.ParameterName = "@unPreferredPersonId";
                    mergeUnPreferredPersonId.Value = request.unPreferredPersonId;


                    var userId = new SqlParameter();
                    userId.SqlDbType = SqlDbType.Int;
                    userId.ParameterName = "@userid";
                    userId.Value = request.userid;


                    _unitOfWork.Context.Database.SetCommandTimeout(3600);
                    var duplicatePersons = await _unitOfWork.Context.Query<MergePersonsPoco>().FromSql(sql.ToString(), parameters: new[]
                    {
                        mergePreferredPersonId,
                        mergeUnPreferredPersonId,
                        userId
                    }).AsNoTracking().ToListAsync();

                    return Result<MergeDuplicateRecordsResponse>.Valid(new MergeDuplicateRecordsResponse()
                    {
                        Message = $"Successfully merged patient records"
                    });
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"An error occured while trying to merge duplicate persons");
                    return Result<MergeDuplicateRecordsResponse>.Invalid($"An error occured while trying to merge duplicate persons");
                }
            }
        }
    }
}
