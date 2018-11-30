using AutoMapper;
using IQCare.Common.Core.Models;
using IQCare.PMTCT.BusinessProcess.Queries;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;

namespace IQCare.PMTCT.BusinessProcess.QueryHandlers
{
    public class GetPatientChronicIllnessInfoQueryHandler : IRequestHandler<GetPatientChronicIllnessInfo, Result<IEnumerable<PatientChronicIllnessViewModel>>>
    {
        readonly IPmtctUnitOfWork _pmtctUnitOfWork;
        readonly IMapper _mapper;
        public GetPatientChronicIllnessInfoQueryHandler(IPmtctUnitOfWork pmtctUnitOfWork, IMapper mapper)
        {
            _pmtctUnitOfWork = pmtctUnitOfWork;
            _mapper = mapper;
        }
        public Task<Result<IEnumerable<PatientChronicIllnessViewModel>>> Handle(GetPatientChronicIllnessInfo request, CancellationToken cancellationToken)
        {
            try
            {
                var patientChronicIllnesses = _pmtctUnitOfWork.Repository<PatientChronicIllnessView>().Get(x => x.PatientId == request.PatientId).AsEnumerable();

                var chronicIllnessesModel = _mapper.Map<IEnumerable<PatientChronicIllnessViewModel>>(patientChronicIllnesses);

                return Task.FromResult(Result<IEnumerable<PatientChronicIllnessViewModel>>.Valid(chronicIllnessesModel));
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occured while getting patient chronicIllnesses with Id {request.PatientId}";
                Log.Error(ex, errorMessage);
                return Task.FromResult(Result<IEnumerable<PatientChronicIllnessViewModel>>.Invalid(errorMessage));
            }
        }
    }
}
