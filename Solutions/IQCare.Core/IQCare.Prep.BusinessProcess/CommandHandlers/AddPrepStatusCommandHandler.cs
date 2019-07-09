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
    public class AddPrepStatusCommandHandler : IRequestHandler<AddPrepStatusCommand, Result<PatientPrEPStatus>>
    {
        private readonly IPrepUnitOfWork _prepUnitOfWork;
        private readonly IMapper _mapper;

        public AddPrepStatusCommandHandler(IPrepUnitOfWork prepUnitOfWork, IMapper mapper)
        {
            _prepUnitOfWork = prepUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PatientPrEPStatus>> Handle(AddPrepStatusCommand request, CancellationToken cancellationToken)
        {
            using (_prepUnitOfWork)
            {
                try
                {
                    if (request.Id.HasValue && request.Id.Value > 0)
                    {
                        var result = await _prepUnitOfWork.Repository<PatientPrEPStatus>()
                            .FindByIdAsync(request.Id.Value);
                        if (result != null)
                        {
                            result.AdherenceCounsellingDone = request.AdherenceCounsellingDone;
                            result.SignsOrSymptomsHIV = request.SignsOrSymptomsHIV;
                            result.ContraindicationsPrepPresent = request.ContraindicationsPrepPresent;
                            result.PrepStatusToday = request.PrepStatusToday;
                            result.CreatedBy = request.CreatedBy;
                            result.CondomsIssued = request.CondomsIssued;
                            result.NoOfCondoms = request.NoOfCondoms;

                            _prepUnitOfWork.Repository<PatientPrEPStatus>().Update(result);
                            await _prepUnitOfWork.SaveAsync();

                            return Result<PatientPrEPStatus>.Valid(result);
                        }
                        else
                        {
                            PatientPrEPStatus patientPrEpStatus = new PatientPrEPStatus()
                            {
                                PatientId = request.PatientId,
                                PatientEncounterId = request.PatientEncounterId,
                                SignsOrSymptomsHIV = request.SignsOrSymptomsHIV,
                                AdherenceCounsellingDone = request.AdherenceCounsellingDone,
                                ContraindicationsPrepPresent = request.ContraindicationsPrepPresent,
                                PrepStatusToday = request.PrepStatusToday,
                                DeleteFlag = false,
                                CreatedBy = request.CreatedBy,
                                CreateDate = DateTime.Now,
                                CondomsIssued = request.CondomsIssued,
                                NoOfCondoms = request.NoOfCondoms
                            };

                            await _prepUnitOfWork.Repository<PatientPrEPStatus>().AddAsync(patientPrEpStatus);
                            await _prepUnitOfWork.SaveAsync();

                            return Result<PatientPrEPStatus>.Valid(patientPrEpStatus);
                        }
                    }
                    else
                    {
                        PatientPrEPStatus patientPrEpStatus = new PatientPrEPStatus()
                        {
                            PatientId = request.PatientId,
                            PatientEncounterId = request.PatientEncounterId,
                            SignsOrSymptomsHIV = request.SignsOrSymptomsHIV,
                            AdherenceCounsellingDone = request.AdherenceCounsellingDone,
                            ContraindicationsPrepPresent = request.ContraindicationsPrepPresent,
                            PrepStatusToday = request.PrepStatusToday,
                            DeleteFlag = false,
                            CreatedBy = request.CreatedBy,
                            CreateDate = DateTime.Now,
                        };

                        await _prepUnitOfWork.Repository<PatientPrEPStatus>().AddAsync(patientPrEpStatus);
                        await _prepUnitOfWork.SaveAsync();

                        return Result<PatientPrEPStatus>.Valid(patientPrEpStatus);
                    }
                }
                catch (Exception ex)
                {
                    string message = $"An error occured while saving prep status request for patientId {request.PatientId}";
                    Log.Error(ex, message);

                    return Result<PatientPrEPStatus>.Invalid(message);
                }
            }
        }
    }
}