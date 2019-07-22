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
    public class GetFamilyPlanningQueryHandler: IRequestHandler<GetPatientFamilyPlanningQuery, Result<List<PatientFamilyPlanningViewModel>>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<GetPatientDiagnosisInfoQueryHandler>();

    public GetFamilyPlanningQueryHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
    {
        _maternityUnitOfWork = maternityUnitOfWork;
        _mapper = mapper;
    }
    public Task<Result<List<PatientFamilyPlanningViewModel>>> Handle(GetPatientFamilyPlanningQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var patientFamilyPlanning = _maternityUnitOfWork.Repository<PatientFamilyPlanning>().Get(x => x.PatientId == request.PatientId
            && x.PatientMasterVisitId == request.PatientMasterVisitId && x.DeleteFlag == false);
            var familyPlanningViewModel = _mapper.Map<List<PatientFamilyPlanningViewModel>>(patientFamilyPlanning);


            return Task.FromResult(Result<List<PatientFamilyPlanningViewModel>>.Valid(familyPlanningViewModel));
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"An error occured while getting patient family planning info{ request.PatientId}");
            return Task.FromResult(Result<List<PatientFamilyPlanningViewModel>>.Invalid(ex.Message));
        }

    }
  }
}