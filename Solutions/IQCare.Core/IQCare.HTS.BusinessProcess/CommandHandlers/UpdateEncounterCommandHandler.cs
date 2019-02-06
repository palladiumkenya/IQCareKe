using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class UpdateEncounterCommandHandler : IRequestHandler<UpdateEncounterCommand, Result<UpdateEncounterResponse>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;
        public UpdateEncounterCommandHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        public async Task<Result<UpdateEncounterResponse>> Handle(UpdateEncounterCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var htsEncounter = await _unitOfWork.Repository<HtsEncounter>().FindByIdAsync(request.encounterId);
                    if (htsEncounter != null)
                    {
                        htsEncounter.EncounterRemarks = request.Encounter.EncounterRemarks;
                        htsEncounter.EverSelfTested = request.Encounter.EverSelfTested;
                        htsEncounter.EverTested = request.Encounter.EverTested;
                        htsEncounter.GeoLocation = request.Encounter.GeoLocation;
                        htsEncounter.MonthSinceSelfTest = request.Encounter.MonthSinceSelfTest;
                        htsEncounter.MonthsSinceLastTest = request.Encounter.MonthsSinceLastTest;
                        htsEncounter.ProviderId = request.Encounter.ProviderId;
                        htsEncounter.TestedAs = request.Encounter.TestedAs;
                        htsEncounter.TestEntryPoint = request.Encounter.TestEntryPoint;
                        htsEncounter.TestingStrategy = request.Encounter.TestingStrategy;
                        htsEncounter.EncounterType = request.Encounter.EncounterType;
                        htsEncounter.HivCounsellingDone = request.Encounter.HivCounsellingDone;


                        _unitOfWork.Repository<HtsEncounter>().Update(htsEncounter);
                        await _unitOfWork.SaveAsync();


                        if (request.Encounter.Disabilities.Any())
                        {
                            var disabilities = await _unitOfWork.Repository<ClientDisability>().Get(x => x.PersonId == request.Encounter.PersonId).ToAsyncEnumerable().ToList();
                            foreach (var disability in disabilities)
                            {
                                disability.DeleteFlag = true;
                                _unitOfWork.Repository<ClientDisability>().Update(disability);
                                await _unitOfWork.SaveAsync();
                            }

                            List<ClientDisability> clientDisabilities = new List<ClientDisability>();
                            request.Encounter.Disabilities.ForEach(x => clientDisabilities.Add(new ClientDisability
                            {
                                DisabilityId = x,
                                PatientEncounterId = htsEncounter.PatientEncounterID,
                                PersonId = request.Encounter.PersonId,
                                DeleteFlag = false,
                                CreateDate = DateTime.Now,
                                CreatedBy = request.Encounter.ProviderId
                            }));

                            await _unitOfWork.Repository<ClientDisability>().AddRangeAsync(clientDisabilities);
                            await _unitOfWork.SaveAsync();
                        }
                    }
                    
                    _unitOfWork.Dispose();

                    return Result<UpdateEncounterResponse>.Valid(new UpdateEncounterResponse()
                    {
                        IsSuccessFul = true
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<UpdateEncounterResponse>.Invalid(e.Message);
                }
            }
        }
    }
}