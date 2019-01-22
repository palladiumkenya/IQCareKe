using IQCare.Common.Core.Models;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class AddEncounterCommandHandler : IRequestHandler<AddEncounterCommand, Result<AddEncounterResponse>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;
        public AddEncounterCommandHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddEncounterResponse>> Handle(AddEncounterCommand request, CancellationToken cancellationToken)
        {
            // TODO: use automapper
            using (var trans = _unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    // create HtsEncounter instance
                    var htsEncounter = new HtsEncounter
                    {
                        EncounterRemarks = request.Encounter.EncounterRemarks,
                        EverSelfTested = request.Encounter.EverSelfTested,
                        EverTested = request.Encounter.EverTested,
                        GeoLocation = request.Encounter.GeoLocation,
                        MonthSinceSelfTest = request.Encounter.MonthSinceSelfTest,
                        MonthsSinceLastTest = request.Encounter.MonthsSinceLastTest,
                        PatientEncounterID = request.Encounter.PatientEncounterID,
                        PersonId = request.Encounter.PersonId,
                        ProviderId = request.Encounter.ProviderId,
                        TestedAs = request.Encounter.TestedAs,
                        TestEntryPoint = request.Encounter.TestEntryPoint,
                        TestingStrategy = request.Encounter.TestingStrategy,
                        EncounterType = request.Encounter.EncounterType,
                        HivCounsellingDone = request.Encounter.HivCounsellingDone
                    };

                    await _unitOfWork.Repository<HtsEncounter>().AddAsync(htsEncounter);
                    await _unitOfWork.SaveAsync();

                    if (request.Encounter.Disabilities.Any())
                    {
                        List<ClientDisability> clientDisabilities = new List<ClientDisability>();
                        request.Encounter.Disabilities.ForEach(x => clientDisabilities.Add(new ClientDisability
                        {
                            DisabilityId = x,
                            PatientEncounterId = request.Encounter.PatientEncounterID,
                            PersonId = request.Encounter.PersonId,
                            CreateDate = DateTime.Now,
                            CreatedBy = request.Encounter.ProviderId,
                            DeleteFlag = false
                        }));
                        await _unitOfWork.Repository<ClientDisability>().AddRangeAsync(clientDisabilities);
                        await _unitOfWork.SaveAsync();
                    }

                    trans.Commit();

                    _unitOfWork.Dispose();

                    return Result<AddEncounterResponse>
                        .Valid(new AddEncounterResponse { HtsEncounterId = htsEncounter.Id });
                }

                catch(Exception ex)
                {
                    trans.Rollback();
                    Log.Error(ex.Message);
                    // TODO:  log error
                    return Result<AddEncounterResponse>.Invalid(ex.Message);
                }
            }

        }
    }
}
