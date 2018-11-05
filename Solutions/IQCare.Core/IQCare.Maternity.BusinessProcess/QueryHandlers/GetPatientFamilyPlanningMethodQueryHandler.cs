using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Queries.PNC;
using AutoMapper;
using IQCare.Maternity.Core.Domain.PNC;

namespace IQCare.Maternity.BusinessProcess.QueryHandlers
{
    class GetPatientFamilyPlanningMethodQueryHandler : IRequestHandler<GetPatientFamilyPlanningMethodQuery, Result<List<PatientFamilyPlanningMethodViewModel>>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<GetPatientDiagnosisInfoQueryHandler>();

        public GetPatientFamilyPlanningMethodQueryHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }
        public Task<Result<List<PatientFamilyPlanningMethodViewModel>>> Handle(GetPatientFamilyPlanningMethodQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var patientFamilyPlanning = _maternityUnitOfWork.Repository<PatientFamilyPlanningMethodView>().Get(x => x.PatientId == request.PatientId)
                        .AsEnumerable();
                var familyPlanningViewModel = _mapper.Map<List<PatientFamilyPlanningMethodViewModel>>(patientFamilyPlanning);


                return Task.FromResult(Result<List<PatientFamilyPlanningMethodViewModel>>.Valid(familyPlanningViewModel));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occured while getting patient family planning methods{ request.PatientId}");
                return Task.FromResult(Result<List<PatientFamilyPlanningMethodViewModel>>.Invalid(ex.Message));
            }

        }
    }
}
