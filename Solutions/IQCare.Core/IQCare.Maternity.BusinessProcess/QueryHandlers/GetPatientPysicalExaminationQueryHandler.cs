using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Queries.PNC;
using IQCare.Maternity.Core.Domain.PNC;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace IQCare.Maternity.BusinessProcess.QueryHandlers
{
    public class GetPatientPysicalExaminationQueryHandler : IRequestHandler<GetPhysicalExaminationQuery, Result<List<GetPatientPhysicalExaminationViewModel>>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<GetPatientPysicalExaminationQueryHandler>();

        public GetPatientPysicalExaminationQueryHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }
        public Task<Result<List<GetPatientPhysicalExaminationViewModel>>> Handle(GetPhysicalExaminationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var patientExamination = _maternityUnitOfWork.Repository<PhysicalExaminationView>().Get(x => x.PatientId == request.PatientId);
                var familyPlanningViewModel = _mapper.Map<List<GetPatientPhysicalExaminationViewModel>>(patientExamination);


                return Task.FromResult(Result<List<GetPatientPhysicalExaminationViewModel>>.Valid(familyPlanningViewModel));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occured while fetching patient physical exammination info{ request.PatientId}");
                return Task.FromResult(Result<List<GetPatientPhysicalExaminationViewModel>>.Invalid(ex.Message));
            }

        }
    }
}