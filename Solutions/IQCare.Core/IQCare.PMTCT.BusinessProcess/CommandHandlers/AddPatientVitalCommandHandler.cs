using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class AddPatientVitalCommandHandler : IRequestHandler<AddPatientVitalCommand,Result<object>>
    {
        private readonly IPmtctUnitOfWork _pmtctUnitOfWork;
        private readonly IMapper _mapper;
        public AddPatientVitalCommandHandler(IPmtctUnitOfWork pmtctUnitOfWork, IMapper mapper)
        {
            _pmtctUnitOfWork = pmtctUnitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<object>> Handle(AddPatientVitalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patientVital = _mapper.Map<PatientVital>(request);
                await _pmtctUnitOfWork.Repository<PatientVital>().AddAsync(patientVital);
                await _pmtctUnitOfWork.SaveAsync();

                return Result<object>.Valid(new { patientVital.Id });
            }
            catch (Exception ex)
            {
                string message =
                    $"An error occured while adding patient vital details for patient master visit {request.PatientMasterVisitId}";
                 Log.Error(message,ex);              
                return  Result<object>.Invalid(message);
            }
        }
    }
}
