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
    public class GetPharmacyCurrentRegimenCommandHandler :IRequestHandler<GetPharmacyCurrentRegimenCommand,Result<GetPharmacyCurrentRegimenResponse>>
    {
        IPharmUnitOfWork _pharmUnitOfWork;
        public GetPharmacyCurrentRegimenCommandHandler(IPharmUnitOfWork pharmUnitOfWork)
        {
            _pharmUnitOfWork = pharmUnitOfWork ?? throw new ArgumentNullException(nameof(pharmUnitOfWork));
        }

        public async Task<Result<GetPharmacyCurrentRegimenResponse>> Handle(GetPharmacyCurrentRegimenCommand request, CancellationToken cancellationToken)
        {
            using (_pharmUnitOfWork)
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("sp_getCurrentRegimen @PatientID");

                    var patientId = new SqlParameter("@PatientID", request.PatientId);
                    
                    var currentregimenlist = await _pharmUnitOfWork.Context.Query<PatientCurrentRegimenTracker>().FromSql(sql.ToString(),
                        parameters: new[]
                        {
                            patientId
                            
                        }).ToListAsync();

                    List<PharmacyFields> lst = new List<PharmacyFields>();
                    if(currentregimenlist.Count > 0)
                    {
                        currentregimenlist.ForEach(x =>
                         {
                             PharmacyFields flds = new PharmacyFields();
                             flds.RegimenLine = x.RegimenLineId.ToString();
                             flds.Regimen = x.RegimenId.ToString();
                             lst.Add(flds);
                         });
                       
                    }

                    return Result<GetPharmacyCurrentRegimenResponse>.Valid(new GetPharmacyCurrentRegimenResponse()
                    {
                        pharmacyFields = lst
                    });
                }

                catch (Exception ex)
                {
                    return Result<GetPharmacyCurrentRegimenResponse>.Invalid(ex.Message);
                }

            }

        }

    }
}
