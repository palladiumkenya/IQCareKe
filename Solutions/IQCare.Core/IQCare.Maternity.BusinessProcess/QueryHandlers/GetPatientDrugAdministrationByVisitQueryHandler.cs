using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Queries.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Maternity.BusinessProcess.QueryHandlers
{
    public class GetPatientDrugAdministrationByVisitQueryHandler : IRequestHandler<GetPatientDrugAdministrationByVisitInfoQuery, Result<List<PatientDrugAdministrationView>>>
    {
        private readonly ILogger _logger = Log.ForContext<GetPatientDrugAdministrationInfoQueryHandler>();
        private readonly IMaternityUnitOfWork _maternityUnitOfWork;
        private readonly IMapper _mapper;

        public GetPatientDrugAdministrationByVisitQueryHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }


        public async Task<Result<List<PatientDrugAdministrationView>>> Handle(GetPatientDrugAdministrationByVisitInfoQuery request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    var patientDrugAdministration = await _maternityUnitOfWork.Repository<PatientDrugAdministrationView>()
                        .Get(x => x.PatientId == request.PatientId &&
                                  x.PatientMasterVisitId == request.PatientMasterVisitId && x.DeleteFlag == false)
                        .ToListAsync();

                    return Result<List<PatientDrugAdministrationView>>.Valid(patientDrugAdministration);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"An error occured while getting patient drug administration for Id {request.PatientId} and PatientMasterVisitId {request.PatientMasterVisitId}");
                    return Result<List<PatientDrugAdministrationView>>.Invalid(ex.Message);
                }
            }
        }
    }
}