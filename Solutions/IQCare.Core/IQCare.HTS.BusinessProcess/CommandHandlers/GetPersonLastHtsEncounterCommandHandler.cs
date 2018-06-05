using IQCare.Common.Core.Models;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Infrastructure;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class GetPersonLastHtsEncounterCommandHandler : IRequestHandler<GetPersonLastHtsEncounterCommand, Result<GetPersonLastHtsEncounterResponse>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;
        private readonly ICommonUnitOfWork _commonUnitOfWork;

        public GetPersonLastHtsEncounterCommandHandler(IHTSUnitOfWork unitOfWork, ICommonUnitOfWork commonUnitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<Result<GetPersonLastHtsEncounterResponse>> Handle(GetPersonLastHtsEncounterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    var result = await _unitOfWork.Repository<HtsEncounter>().Get(x => x.PersonId == request.PersonId)
                        .OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                    var patientEncounter = new PatientEncounter();
                    int encounterId = 0;
                    if (result != null)
                    {
                        patientEncounter = await _commonUnitOfWork.Repository<PatientEncounter>()
                        .Get(x => x.Id == result.PatientEncounterID).FirstOrDefaultAsync();

                        encounterId = result.Id;
                    }
                    
                    _unitOfWork.Dispose();
                    _commonUnitOfWork.Dispose();

                    var personLastHtsEncounter = new GetPersonLastHtsEncounterResponse() {
                        EncounterId = encounterId,
                        PatientMasterVisitId = patientEncounter.PatientMasterVisitId,
                        PatientEncounterID = patientEncounter.Id
                        
                    };

                    return Result<GetPersonLastHtsEncounterResponse>.Valid(personLastHtsEncounter);
                }
            }
            catch (Exception e)
            {
                return Result<GetPersonLastHtsEncounterResponse>.Invalid(e.Message);
            }
        }
    }
}