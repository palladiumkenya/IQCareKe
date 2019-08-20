using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiDeliveryMaternalHistory;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiDeliveryMaternalHistory
{
    public class GetMaternalViralLoadCommandHandler : IRequestHandler<GetMaternalViralLoadCommand, Result<GetMaternalViralLoadResult>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;
        public GetMaternalViralLoadCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<GetMaternalViralLoadResult>> Handle(GetMaternalViralLoadCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("sp_getAllViralLoads @PatientId");

                    var patientIdParam = new SqlParameter("@PatientId", request.PatientId);

                    var patientViralLoads = await _unitOfWork.Context.Query<PatientViralLoadPoco>().FromSql(sql.ToString(),
                        parameters: new[]
                        {
                            patientIdParam
                        }).ToListAsync();

                    return Result<GetMaternalViralLoadResult>.Valid(new GetMaternalViralLoadResult()
                    {
                        PatientViralLoad = patientViralLoads
                    });
                }
                catch (Exception e)
                {
                    Log.Error($"Error fetching viralloads for PatientId: {request.PatientId}");
                    return Result<GetMaternalViralLoadResult>.Invalid(e.Message);
                }
            }
        }
    }
}