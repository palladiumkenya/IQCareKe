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
using Newtonsoft.Json;
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

                    var afyaMobileMessage = await registerPersonService.AddAfyaMobileInbox(DateTime.Now, request.MESSAGE_HEADER.MESSAGE_TYPE, afyaMobileId, JsonConvert.SerializeObject(request), false);

                    var indexClientIdentifiers = await registerPersonService.getPersonIdentifiers(indexClientAfyaMobileId, 10);
                    if (indexClientIdentifiers.Count > 0)
                    {
                        //Get Index client
                        var indexClient = await registerPersonService.GetPatientByPersonId(indexClientIdentifiers[0].PersonId);
                        var partnetPersonIdentifiers = await registerPersonService.getPersonIdentifiers(afyaMobileId, 10);
                        if (partnetPersonIdentifiers.Count > 0)
                        {
                            DateTime screeningDate = DateTime.Now;
                            try
                            {
                                screeningDate = DateTime.ParseExact(request.SCREENING_ENCOUNTER.FAMILY_SCREENING.SCREENING_DATE, "yyyyMMdd", null);
                            }
                            catch (Exception e)
                            {
                                Log.Error($"Could not parse family screening SCREENING_DATE: {request.SCREENING_ENCOUNTER.FAMILY_SCREENING.SCREENING_DATE} as a valid date: Incorrect format, date should be in the following format yyyyMMdd");
                                throw new Exception($"Could not parse family screening SCREENING_DATE: {request.SCREENING_ENCOUNTER.FAMILY_SCREENING.SCREENING_DATE} as a valid date: Incorrect format, date should be in the following format yyyyMMdd");
                            }
                            int hivStatus = request.SCREENING_ENCOUNTER.FAMILY_SCREENING.HIV_STATUS;
                            int eligible = request.SCREENING_ENCOUNTER.FAMILY_SCREENING.ELIGIBLE_FOR_HTS;
                            DateTime bookingDate = DateTime.Now;
                            try
                            {
                                bookingDate = DateTime.ParseExact(request.SCREENING_ENCOUNTER.FAMILY_SCREENING.BOOKING_DATE, "yyyyMMdd", null);
                            }
                            catch (Exception e)
                            {
                                Log.Error($"Could not parse family screening BOOKING_DATE: {request.SCREENING_ENCOUNTER.FAMILY_SCREENING.BOOKING_DATE} as a valid date: Incorrect format, date should be in the following format yyyyMMdd");
                                throw new Exception($"Could not parse family screening BOOKING_DATE: {request.SCREENING_ENCOUNTER.FAMILY_SCREENING.BOOKING_DATE} as a valid date: Incorrect format, date should be in the following format yyyyMMdd. Exception: {e.Message}");
                            }
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

                            var stringParnerObject = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                familyId = partnetPersonIdentifiers[0].PersonId,
                                familyTraced = true
                            });

                            var partnerScreeningDone =
                                await registerPersonService.AddAppStateStore(indexClient.PersonId, indexClient.Id, 10,
                                    null, null, stringParnerObject);

                            var familyHivStatus = await _unitOfWork.Repository<LookupItemView>()
                                .Get(x => x.MasterName == "ScreeningHivStatus" && x.ItemId == hivStatus).ToListAsync();

                            if (familyHivStatus.Count > 0 && familyHivStatus[0].ItemName == "Positive")
                            {
                                var stringFamilyScreenedPositiveObject = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    familyId = partnetPersonIdentifiers[0].PersonId,
                                    familyTraced = true
                                });

                                var hasFamiyBeenScreenedPositive = await registerPersonService.AddAppStateStore(indexClient.PersonId, indexClient.Id, 14,
                                        null, null, stringFamilyScreenedPositiveObject);
                            }
                        }
                        else
                        {
                            //update message has been processed
                            await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Family member with afyamobileid: {afyaMobileId} could not be found", false);
                            return Result<string>.Invalid($"Family member with afyamobileid: {afyaMobileId} could not be found");
                        }
                    }
                    else
                    {
                        //update message has been processed
                        await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Index client with afyamobileid: {indexClientAfyaMobileId} could not be found for family member: {afyaMobileId}", false);
                        return Result<string>.Invalid($"Index client with afyamobileid: {indexClientAfyaMobileId} could not be found for family member: {afyaMobileId}");
                    }

                    //update message has been processed
                    await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Successfully synchronized family screening for clientid: {indexClientAfyaMobileId} and  partnerid: {afyaMobileId}", true);
                    trans.Commit();
                    return Result<string>.Valid($"Successfully synchronized family screening for afyamobileId: {afyaMobileId}");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Log.Error($"Failed to synchronize family screening for clientId: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                    return Result<string>.Invalid($"Failed to synchronize family screening for clientId: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                }
            }
        }
    }
}