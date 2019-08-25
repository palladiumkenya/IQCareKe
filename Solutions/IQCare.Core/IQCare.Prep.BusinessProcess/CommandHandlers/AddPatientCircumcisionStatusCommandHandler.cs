using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;
using IQCare.Prep.BusinessProcess.Commands;
using IQCare.Prep.Core.Models;
using IQCare.Prep.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;

namespace IQCare.Prep.BusinessProcess.CommandHandlers
{
    public class AddPatientCircumcisionStatusCommandHandler : IRequestHandler<AddPatientCircumcisionStatusCommand, Result<PatientCircumcisionStatus>>
    {
        private readonly IPrepUnitOfWork _prepUnitOfWork;
        private readonly IMapper _mapper;

        public AddPatientCircumcisionStatusCommandHandler(IPrepUnitOfWork prepUnitOfWork, IMapper mapper)
        {
            _prepUnitOfWork = prepUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PatientCircumcisionStatus>> Handle(AddPatientCircumcisionStatusCommand request, CancellationToken cancellationToken)
        {
            using (_prepUnitOfWork)
            {
                try
                {
                    if (request.Id.HasValue && request.Id.Value > 0)
                    {
                        var result = await _prepUnitOfWork.Repository<PatientCircumcisionStatus>()
                            .FindByIdAsync(request.Id.Value);
                        if (result != null)
                        {
                            result.ClientCircumcised = request.ClientCircumcised;
                            result.ReferredToVMMC = request.ReferredToVMMC;

                            _prepUnitOfWork.Repository<PatientCircumcisionStatus>().Update(result);
                            await _prepUnitOfWork.SaveAsync();

                            return Result<PatientCircumcisionStatus>.Valid(result);
                        }
                    }

                    PatientCircumcisionStatus patientCircumcisionStatus = new PatientCircumcisionStatus()
                    {
                        PatientId = request.PatientId,
                        ClientCircumcised = request.ClientCircumcised,
                        ReferredToVMMC = request.ReferredToVMMC,
                        CreateDate = request.CreateDate,
                        CreatedBy = request.CreatedBy,
                        DeleteFlag = request.DeleteFlag
                    };

                    await _prepUnitOfWork.Repository<PatientCircumcisionStatus>().AddAsync(patientCircumcisionStatus);
                    await _prepUnitOfWork.SaveAsync();

                    return Result<PatientCircumcisionStatus>.Valid(patientCircumcisionStatus);
                }
                catch (Exception ex)
                {
                    string message = $"An error occured while saving prep circumcision status request for patientId {request.PatientId}";
                    Log.Error(ex, message);

                    return Result<PatientCircumcisionStatus>.Invalid(message);
                }
            }
        }
    }
}