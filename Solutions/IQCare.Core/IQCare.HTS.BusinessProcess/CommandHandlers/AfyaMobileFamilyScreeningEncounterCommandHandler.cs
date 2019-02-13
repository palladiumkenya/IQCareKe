using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.BusinessProcess.Services;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class AfyaMobileFamilyScreeningEncounterCommandHandler : IRequestHandler<AfyaMobileFamilyScreeningEncounterCommand, Result<string>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        private readonly IHTSUnitOfWork _htsUnitOfWork;

        public AfyaMobileFamilyScreeningEncounterCommandHandler(IHTSUnitOfWork htsUnitOfWork, ICommonUnitOfWork commonUnitOfWork)
        {
            _htsUnitOfWork = htsUnitOfWork ?? throw new ArgumentNullException(nameof(htsUnitOfWork));
            _unitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<Result<string>> Handle(AfyaMobileFamilyScreeningEncounterCommand request, CancellationToken cancellationToken)
        {
            string afyaMobileId = string.Empty;
            string indexClientAfyaMobileId = string.Empty;

            using (var trans = _htsUnitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                    EncounterTestingService encounterTestingService = new EncounterTestingService(_unitOfWork, _htsUnitOfWork);

                    for (int j = 0; j < request.INTERNAL_PATIENT_ID.Count; j++)
                    {
                        if (request.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE == "AFYA_MOBILE_ID")
                        {
                            afyaMobileId = request.INTERNAL_PATIENT_ID[j].ID;
                        }

                        if (request.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE == "INDEX_CLIENT_AFYAMOBILE_ID")
                        {
                            indexClientAfyaMobileId = request.INTERNAL_PATIENT_ID[j].ID;
                        }
                    }

                    var indexClientIdentifiers = await registerPersonService.getPersonIdentifiers(indexClientAfyaMobileId, 10);
                    if (indexClientIdentifiers.Count > 0)
                    {
                        //Get Index client
                        var indexClient = await registerPersonService.GetPatientByPersonId(indexClientIdentifiers[0].PersonId);
                        var partnetPersonIdentifiers = await registerPersonService.getPersonIdentifiers(afyaMobileId, 10);
                        if (partnetPersonIdentifiers.Count > 0)
                        {
                            DateTime screeningDate = DateTime.ParseExact(request.SCREENING_ENCOUNTER.FAMILY_SCREENING.SCREENING_DATE, "yyyyMMdd", null);
                            int hivStatus = request.SCREENING_ENCOUNTER.FAMILY_SCREENING.HIV_STATUS;
                            int eligible = request.SCREENING_ENCOUNTER.FAMILY_SCREENING.ELIGIBLE_FOR_HTS;
                            DateTime bookingDate = DateTime.ParseExact(request.SCREENING_ENCOUNTER.FAMILY_SCREENING.BOOKING_DATE, "yyyyMMdd", null);
                            string remarks = request.SCREENING_ENCOUNTER.FAMILY_SCREENING.REMARKS;

                            var familyScreenings = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "FamilyScreening")
                                .ToListAsync();

                            List<Screening> familyScreeningList = new List<Screening>();
                            for (int j = 0; j < familyScreenings.Count; j++)
                            {
                                if (familyScreenings[j].ItemName == "EligibleTesting")
                                {
                                    Screening screening = new Screening()
                                    {
                                        ScreeningCategoryId = familyScreenings[j].ItemId,
                                        ScreeningTypeId = familyScreenings[j].MasterId,
                                        ScreeningValueId = eligible
                                    };
                                    familyScreeningList.Add(screening);
                                }
                                else if (familyScreenings[j].ItemName == "ScreeningHivStatus")
                                {
                                    Screening screening = new Screening()
                                    {
                                        ScreeningCategoryId = familyScreenings[j].ItemId,
                                        ScreeningTypeId = familyScreenings[j].MasterId,
                                        ScreeningValueId = hivStatus
                                    };
                                    familyScreeningList.Add(screening);
                                }
                            }

                            var patientMasterVisitEntity = await _unitOfWork.Repository<PatientMasterVisit>()
                                .Get(x => x.PatientId == indexClient.Id && x.ServiceId == 2).ToListAsync();

                            int patientMasterVisitId = patientMasterVisitEntity.OrderBy(x => x.Id).FirstOrDefault().Id;

                            var familyScreeningReturnValue = await encounterTestingService.AddPartnerScreening(partnetPersonIdentifiers[0].PersonId, indexClient.Id, patientMasterVisitId, null,
                                    screeningDate, bookingDate, familyScreeningList, 1);
                        }
                        else
                        {
                            return Result<string>.Invalid($"Family member with afyamobileid: {afyaMobileId} could not be found");
                        }
                    }
                    else
                    {
                        return Result<string>.Invalid($"Index client with afyamobileid: {indexClientAfyaMobileId} could not be found for family member: {afyaMobileId}");
                    }

                    return Result<string>.Valid($"Successfully synchronized family screening for afyamobileId: {afyaMobileId}");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Log.Error(ex.Message);
                    return Result<string>.Invalid($"Failed to synchronize partner screening for clientId: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                }
            }
        }
    }
}