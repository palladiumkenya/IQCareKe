using System;
using System.Collections.Generic;
using System.Linq;
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

    public class UpdatePatientVitalCommandHandler : IRequestHandler<UpdatePatientVitalCommand, Result<object>>
    {
        private readonly IPmtctUnitOfWork _pmtctUnitOfWork;

        public UpdatePatientVitalCommandHandler(IPmtctUnitOfWork pmtctUnitOfWork)
        {
            _pmtctUnitOfWork = pmtctUnitOfWork;
            
        }
        public async Task<Result<object>> Handle(UpdatePatientVitalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patientVitalInfoToUpdate = _pmtctUnitOfWork.Repository<PatientVital>().Get(x => x.Id == request.PatientVitalInfo.Id)
                        .SingleOrDefault();
                if (patientVitalInfoToUpdate == null)
                    return Result<object>.Invalid($"Patient vitals information with Id {request.PatientVitalInfo.Id} not found");

                patientVitalInfoToUpdate.UpdateVitalsInfo(request.PatientVitalInfo);
                _pmtctUnitOfWork.Repository<PatientVital>().Update(patientVitalInfoToUpdate);
                await _pmtctUnitOfWork.SaveAsync();

                return Result<object>.Valid(new { Message = "Succussfully updated patient vitals information", PatientVitalId = request.PatientVitalInfo.Id });
            }
            catch (Exception ex)
            {
                string errorMessage =$"An error occured while updating patient vital information with Id {request.PatientVitalInfo.Id}";

                Log.Error(ex, errorMessage);
               return Result<object>.Invalid(errorMessage);
            }
        }
    }
}
