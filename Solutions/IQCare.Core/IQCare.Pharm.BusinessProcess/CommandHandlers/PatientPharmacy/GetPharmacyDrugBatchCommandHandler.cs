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
   
    public class GetPharmacyDrugBatchCommandHandler:IRequestHandler<GetPharmacyDrugBatchCommand,Result<GetPharmacyDrugBatchResponse>>
    {
        IPharmUnitOfWork _pharmUnitOfWork;
        public GetPharmacyDrugBatchCommandHandler(IPharmUnitOfWork pharmUnitOfWork)
        {
            _pharmUnitOfWork = pharmUnitOfWork ?? throw new ArgumentNullException(nameof(pharmUnitOfWork));
        }

        public  async Task<Result<GetPharmacyDrugBatchResponse>>  Handle(GetPharmacyDrugBatchCommand request, CancellationToken cancellationToken)
        {

            using (_pharmUnitOfWork)
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("sp_getPharmacyBatch @DrugPk");

                    var drugpk = new SqlParameter("@DrugPk", request.Drug_Pk);

                    var batches = await _pharmUnitOfWork.Context.Query<DrugBatch>().FromSql(sql.ToString(),
                        parameters: new[]
                        {
                            drugpk

                        }).ToListAsync();



                    return Result<GetPharmacyDrugBatchResponse>.Valid(new GetPharmacyDrugBatchResponse()
                    {

                        drugBatches=batches
                    }
                    );
                }

                catch (Exception ex)
                {
                    return Result<GetPharmacyDrugBatchResponse>.Invalid(ex.Message);
                }

            }
        }
    }
}
