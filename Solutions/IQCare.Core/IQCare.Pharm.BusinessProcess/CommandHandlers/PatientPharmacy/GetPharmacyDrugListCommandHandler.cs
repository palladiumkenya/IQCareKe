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
using System.Linq;


namespace IQCare.Pharm.BusinessProcess.CommandHandlers.PatientPharmacy
{
   public  class GetPharmacyDrugListCommandHandler :IRequestHandler<GetPharmacyDrugListCommand,Result<GetPharmacyDrugListResponse>>
    {
        IPharmUnitOfWork _pharmUnitOfWork;
        public GetPharmacyDrugListCommandHandler(IPharmUnitOfWork pharmUnitOfWork)
        {
            _pharmUnitOfWork = pharmUnitOfWork ?? throw new ArgumentNullException(nameof(pharmUnitOfWork));
        }

        public async Task<Result<GetPharmacyDrugListResponse>> Handle(GetPharmacyDrugListCommand request, CancellationToken cancellationToken)
        {

            using (_pharmUnitOfWork)
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("sp_getPharmacyDrugList @pmscm ,@tp");

                    var pmscm = new SqlParameter("@pmscm", request.pmscm);
                    var tp = new SqlParameter("@tp", request.tp);

                    var patientdruglist = await _pharmUnitOfWork.Context.Query<DrugListPoco>().FromSql(sql.ToString(),
                        parameters: new[]
                        {
                            pmscm,
                            tp
                        }).ToListAsync();

                    if ((!(String.IsNullOrEmpty(request.filteritem )) == true) && request.filteritem != "null")
                    {
                        patientdruglist = patientdruglist.FindAll(x => x.val.ToLower().Contains(request.filteritem.ToLower()) || x.val.ToLower().Contains(request.filteritem.ToLower())).ToList();
                           // patientdruglist.Where(x => x.drugName.Contains(request.filteritem) || x.val.Contains(request.filteritem)).ToList();
                    }

                    return Result<GetPharmacyDrugListResponse>.Valid(new GetPharmacyDrugListResponse()
                    {
                        DrugList = patientdruglist
                    });
                }

                catch (Exception ex)
                {
                    return Result<GetPharmacyDrugListResponse>.Invalid(ex.Message);
                }
            
            }
        }
    }
}
