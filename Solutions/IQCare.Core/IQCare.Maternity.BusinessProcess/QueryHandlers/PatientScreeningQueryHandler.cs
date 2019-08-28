
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
    public class PatientScreeningQueryHandler : IRequestHandler<PatientScreeningDetailsQuery, Result<List<PatientScreeningDetailsViewModel>>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<PatientScreeningQueryHandler>();

        public PatientScreeningQueryHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }
        public Task<Result<List<PatientScreeningDetailsViewModel>>> Handle(PatientScreeningDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var patientScreening = _maternityUnitOfWork.Repository<PatientScreeningDetailsView>().Get(x => x.PatientId == request.PatientId && x.PatientMasterVisitId==request.PatientMasterVisitId && x.DeleteFlag ==false);
                var screeningDetails = _mapper.Map<List<PatientScreeningDetailsViewModel>>(patientScreening);

                return Task.FromResult(Result<List<PatientScreeningDetailsViewModel>>.Valid(screeningDetails));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occured while fetching patient screening details info{ request.PatientId}");
                return Task.FromResult(Result<List<PatientScreeningDetailsViewModel>>.Invalid(ex.Message));
            }

        }
    }
}