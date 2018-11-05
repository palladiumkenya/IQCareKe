using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.Core.Domain.PNC;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Maternity.BusinessProcess.QueryHandlers
{
    public class GetPatientPartnerTestingQueryHandler : IRequestHandler<GetPatientPartnerTestingQuery, Result<List<PatientPartnerTestinViewModel>>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<GetPatientDiagnosisInfoQueryHandler>();

    public GetPatientPartnerTestingQueryHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
    {
        _maternityUnitOfWork = maternityUnitOfWork;
        _mapper = mapper;
    }
    public Task<Result<List<PatientPartnerTestinViewModel>>> Handle(GetPatientPartnerTestingQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var patientFamilyPlanning = _maternityUnitOfWork.Repository<PatientPartnerTesting>().Get(x => x.PatientId == request.PatientId);
            var familyPlanningViewModel = _mapper.Map<List<PatientPartnerTestinViewModel>>(patientFamilyPlanning);


            return Task.FromResult(Result<List<PatientPartnerTestinViewModel>>.Valid(familyPlanningViewModel));
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"An error occured while getting patient partner testing info{ request.PatientId}");
            return Task.FromResult(Result<List<PatientPartnerTestinViewModel>>.Invalid(ex.Message));
        }

    }
}
}