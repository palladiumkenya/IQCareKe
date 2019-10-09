using IQCare.Library;
using IQCare.Pharm.BusinessProcess.Commands.PatientPharmacy;
using IQCare.Pharm.Core.Models;
using IQCare.Pharm.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace IQCare.Pharm.BusinessProcess.CommandHandlers.PatientPharmacy
{
    public class GetPharmacyRegimensCommandHandler : IRequestHandler<GetPharmacyRegimensCommand,Result<GetPharmacyRegimensResponse>>
    {

        IPharmUnitOfWork _pharmUnitOfWork;

        public GetPharmacyRegimensCommandHandler(IPharmUnitOfWork pharmUnitOfWork)
        {
            _pharmUnitOfWork = pharmUnitOfWork ?? throw new ArgumentNullException(nameof(pharmUnitOfWork));
        }

        public async Task<Result<GetPharmacyRegimensResponse>> Handle(GetPharmacyRegimensCommand request,CancellationToken cancellationToken)
        {
            using (_pharmUnitOfWork)
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("sp_getPharmacyRegimens @regimenLine");

                    var regimenline = new SqlParameter("@regimenLine", request.LookupName);

                    var currentregimenlist = await _pharmUnitOfWork.Context.Query<Regimen>().FromSql(sql.ToString(),
                        parameters: new[]
                        {
                            regimenline

                        }).ToListAsync();

                    return Result<GetPharmacyRegimensResponse>.Valid(new GetPharmacyRegimensResponse()
                    {
                        Regimens =currentregimenlist
                    });

                }
                catch(Exception ex)
                {

                    return Result<GetPharmacyRegimensResponse>.Invalid(ex.Message);
                }
            }
        }
    }
}
