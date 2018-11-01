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

namespace IQCare.Maternity.BusinessProcess.QueryHandlers
{
    //class GetPatientFamilyPlanningMethodQueryHandler : IRequestHandler<GetPatientFamilyPlanningMethodQuery, Result<List<PatientFamilyPlanningMethodViewModel>>>
    //{
    //    IMaternityUnitOfWork _maternityUnitOfWork;
    //    IMapper _mapper;
    //    ILogger logger = Log.ForContext<GetPatientDiagnosisInfoQueryHandler>();

    //    public GetPatientFamilyPlanningMethodQueryHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
    //    {
    //        _maternityUnitOfWork = maternityUnitOfWork;
    //        _mapper = mapper;
    //    }
    //    //public Task<Result<List<PatientFamilyPlanningMethodViewModel>>> Handle(GetPatientFamilyPlanningMethodQuery request, CancellationToken cancellationToken)
    //    //{
           

    //    //}
    //}
}
