using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Queries;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.QueryHandlers
{
    public class GetPatientVitalsQueryHandler : IRequestHandler<GetPatientVitalsQuery,Result<List<PatientVitalViewModel>>>
    {
        private readonly IPmtctUnitOfWork _pmtctUnitOfWork;
        private readonly IMapper _mapper;

        public GetPatientVitalsQueryHandler(IPmtctUnitOfWork pmtctUnitOfWork, IMapper mapper)
        {
            _pmtctUnitOfWork = pmtctUnitOfWork;
            _mapper = mapper;
        }
        public Task<Result<List<PatientVitalViewModel>>> Handle(GetPatientVitalsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var patientVitals = _pmtctUnitOfWork.Repository<PatientVital>()
                        .Get(x => x.PatientMasterVisitId == request.MasterVisitId).OrderByDescending(x=>x.DateCreated).AsEnumerable();

                var vitalsModel = _mapper.Map<List<PatientVitalViewModel>>(patientVitals);

                return Task.FromResult(Result<List<PatientVitalViewModel>>.Valid(vitalsModel));
            }
            catch (Exception ex)
            {
                string message =
                    $"An error occured while getting patient vitals for master visit Id {request.MasterVisitId}";
                Log.Error(ex, message);

                return Task.FromResult(Result<List<PatientVitalViewModel>>.Invalid(message));
            }
        }
    }
}
