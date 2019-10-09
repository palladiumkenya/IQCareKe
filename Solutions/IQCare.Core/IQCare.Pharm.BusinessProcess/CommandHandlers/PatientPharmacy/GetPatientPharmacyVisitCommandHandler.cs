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
using IQCare.Pharm.BusinessProcess.Services;
using System.Linq;

namespace IQCare.Pharm.BusinessProcess.CommandHandlers.PatientPharmacy
{
    public class GetPatientPharmacyVisitCommandHandler:IRequestHandler<GetPatientPharmacyVisitCommand ,Result<List<PharmacyVisit>>>
    {
        IPharmUnitOfWork _pharmUnitOfWork;
        public GetPatientPharmacyVisitCommandHandler(IPharmUnitOfWork pharmUnitOfWork)
        {
            _pharmUnitOfWork = pharmUnitOfWork ?? throw new ArgumentNullException(nameof(pharmUnitOfWork));
        }

        public async Task<Result<List<PharmacyVisit>>> Handle(GetPatientPharmacyVisitCommand request,CancellationToken cancellationToken)
        {
            using (_pharmUnitOfWork)
            {
                try
                {
                    var visit = await _pharmUnitOfWork.Repository<PharmacyVisit>().Get(x => x.PatientId == request.PatientId).OrderByDescending(x => x.VisitID).ToListAsync();

                    return Result<List<PharmacyVisit>>.Valid(visit);
                   }
                catch(Exception ex)
                {
                    return Result<List<PharmacyVisit>>.Invalid("Error retrieving Visit Details " + ex.Message);
                }
            }
        }
    }
}
