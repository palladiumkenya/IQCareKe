using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;

namespace IQCare.HTS.BusinessProcess.Services
{
    public class EncounterTestingService
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        private readonly IHTSUnitOfWork _htsunitOfWork;
        public EncounterTestingService(ICommonUnitOfWork unitOfWork, IHTSUnitOfWork htsUnitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _htsunitOfWork = htsUnitOfWork ?? throw new ArgumentNullException(nameof(htsUnitOfWork));
        }

        public async Task<PatientLinkage> addLinkage(int personId, DateTime dateEnrolled, string cccNumber, string facility, int userId, string healthWorker, string carde)
        {
            try
            {
                PatientLinkage patientLinkage = new PatientLinkage()
                {
                    PersonId = personId,
                    LinkageDate = dateEnrolled,
                    CCCNumber = cccNumber,
                    Facility = facility,
                    Enrolled = true,
                    DeleteFlag = false,
                    CreatedBy = userId,
                    CreateDate = DateTime.Now,
                    HealthWorker = healthWorker,
                    Cadre = carde
                };

                await _unitOfWork.Repository<PatientLinkage>().AddAsync(patientLinkage);
                await _unitOfWork.SaveAsync();

                return patientLinkage;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Tracing> addTracing(int personId, int tracingType, DateTime tracingDate, int mode, int outcome, int userId, string remarks)
        {
            try
            {
                Tracing pnstrace = new Tracing()
                {
                    PersonID = personId,
                    TracingType = tracingType,
                    DateTracingDone = tracingDate,
                    Mode = mode,
                    Outcome = outcome,
                    Remarks = remarks,
                    DeleteFlag = false,
                    CreatedBy = userId,
                    CreateDate = DateTime.Now
                };

                await _unitOfWork.Repository<Tracing>().AddAsync(pnstrace);
                await _unitOfWork.SaveAsync();

                return pnstrace;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Referral> addReferral(int personId, int fromFacilityId, int serviceAreaId, int referredTo, int referralReason, int userId, DateTime dateToBeEnrolled)
        {
            try
            {
                var referral = new Referral
                {
                    PersonId = personId,
                    ReferralDate = DateTime.Now,
                    FromFacility = fromFacilityId,
                    FromServicePoint = serviceAreaId,
                    ToServicePoint = serviceAreaId,
                    ToFacility = referredTo,
                    ReferralReason = referralReason,
                    ReferredBy = userId,
                    CreatedBy = userId,
                    ExpectedDate = dateToBeEnrolled,
                    CreateDate = DateTime.Now,
                    DeleteFlag = false
                };

                await _htsunitOfWork.Repository<Referral>().AddAsync(referral);
                await _htsunitOfWork.SaveAsync();

                return referral;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<HtsEncounterResult> addHtsEncounterResult(int htsEncounterId, int roundOneTestResult, int? roundTwoTestResult, int? finalResult)
        {
            try
            {
                HtsEncounterResult hTSEncounterResult = new HtsEncounterResult
                {
                    FinalResult = finalResult,
                    HtsEncounterId = htsEncounterId,
                    RoundOneTestResult = roundOneTestResult,
                    RoundTwoTestResult = roundTwoTestResult
                };

                await _unitOfWork.Repository<HtsEncounterResult>().AddAsync(hTSEncounterResult);
                await _unitOfWork.SaveAsync();

                return hTSEncounterResult;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<HtsEncounter> updateHtsEncounter(int htsEncounterId, HtsEncounter htsEncounter)
        {
            try
            {
                var encounter = await _htsunitOfWork.Repository<HtsEncounter>().FindByIdAsync(htsEncounterId);
                encounter.CoupleDiscordant = htsEncounter.CoupleDiscordant;
                encounter.FinalResultGiven = htsEncounter.FinalResultGiven;

                _htsunitOfWork.Repository<HtsEncounter>().Update(encounter);
                await _htsunitOfWork.SaveAsync();
                return encounter;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Core.Model.Testing>> addTesting(List<NewTests> tests, int htsEncounterId, int providerId)
        {
            try
            {
                // Create Testing instances
                List<Core.Model.Testing> testings = new List<Core.Model.Testing>();
                tests.ForEach(t => testings.Add(new Core.Model.Testing
                {
                    ExpiryDate = DateTime.ParseExact(t.EXPIRY_DATE, "yyyyMMdd", null),
                    HtsEncounterId = htsEncounterId,
                    KitId = t.KIT_TYPE,
                    KitLotNumber = t.LOT_NUMBER,
                    Outcome = t.RESULT,
                    ProviderId = providerId,
                    TestRound = t.TEST_ROUND
                }));

                await _htsunitOfWork.Repository<Core.Model.Testing>().AddRangeAsync(testings);
                await _htsunitOfWork.SaveAsync();

                return testings;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<ClientDisability>> addDisabilities(List<int> disabilities, int patientEncounterId, int personId, int providerId)
        {
            try
            {
                List<ClientDisability> clientDisabilities = new List<ClientDisability>();
                if (disabilities.Any())
                {
                    disabilities.ForEach(x => clientDisabilities.Add(new ClientDisability
                    {
                        DisabilityId = x,
                        PatientEncounterId = patientEncounterId,
                        PersonId = personId,
                        CreateDate = DateTime.Now,
                        CreatedBy = providerId,
                        DeleteFlag = false
                    }));
                    await _htsunitOfWork.Repository<ClientDisability>().AddRangeAsync(clientDisabilities);
                    await _htsunitOfWork.SaveAsync();
                }

                return clientDisabilities;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<HtsEncounter> addHtsEncounter(string encounterRemarks, int everSelfTested, int everTested, string geoLocation, 
            int patientEncounterId, int personId, int providerId, int testEntryPoint, int encounterType, int? testingStrategy, int? testedAs,
            int? monthsSinceLastTest, int? monthSinceSelfTest)
        {
            try
            {
                // create HtsEncounter instance
                var htsEncounter = new HtsEncounter
                {
                    EncounterRemarks = encounterRemarks,
                    EverSelfTested = everSelfTested,
                    EverTested = everTested,
                    GeoLocation = geoLocation,
                    MonthSinceSelfTest = monthSinceSelfTest,
                    MonthsSinceLastTest = monthsSinceLastTest,
                    PatientEncounterID = patientEncounterId,
                    PersonId = personId,
                    ProviderId = providerId,
                    TestedAs = testedAs,
                    TestEntryPoint = testEntryPoint,
                    TestingStrategy = testingStrategy,
                    EncounterType = encounterType
                };

                await _htsunitOfWork.Repository<HtsEncounter>().AddAsync(htsEncounter);
                await _htsunitOfWork.SaveAsync();

                return htsEncounter;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PatientScreening> addPatientScreening(int patientId, int patientMasterVisitId, int screeningTypeId, DateTime screeningDate, int screeningValueId, int userId)
        {
            try
            {
                PatientScreening patientScreening = new PatientScreening()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    ScreeningTypeId = screeningTypeId,
                    ScreeningDone = true,
                    ScreeningDate = screeningDate,
                    ScreeningCategoryId = null,
                    ScreeningValueId = screeningValueId,
                    Comment = null,
                    Active = true,
                    DeleteFlag = false,
                    CreatedBy = userId,
                    CreateDate = DateTime.Now,
                    VisitDate = screeningDate
                };

                await _unitOfWork.Repository<PatientScreening>().AddAsync(patientScreening);
                await _unitOfWork.SaveAsync();

                return patientScreening;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PatientConsent> addPatientConsent(int patientId, int patientMasterVisitId, int serviceAreaId, int consentValue, int consentTypeId, DateTime consentDate, int userId, int? declineReason)
        {
            try
            {
                PatientConsent patientConsent = new PatientConsent()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    ServiceAreaId = serviceAreaId,
                    ConsentValue = consentValue,
                    ConsentType = consentTypeId,
                    DeclineReason = declineReason,
                    ConsentDate = consentDate,
                    DeleteFlag = false,
                    CreatedBy = userId,
                    CreateDate = DateTime.Now
                };

                await _unitOfWork.Repository<PatientConsent>().AddAsync(patientConsent);
                await _unitOfWork.SaveAsync();

                return patientConsent;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PatientEncounter> AddPatientEncounter(int patientId, int encounterType, int patientMasterVisitId, DateTime encounterDate, int serviceAreaId, int userId)
        {
            try
            {
                PatientEncounter patientEncounter = new PatientEncounter()
                {
                    PatientId = patientId,
                    EncounterTypeId = encounterType,
                    Status = 0,
                    PatientMasterVisitId = patientMasterVisitId,
                    EncounterStartTime = encounterDate,
                    EncounterEndTime = encounterDate,
                    ServiceAreaId = serviceAreaId,
                    CreatedBy = userId,
                    CreateDate = DateTime.Now
                };

                await _unitOfWork.Repository<PatientEncounter>().AddAsync(patientEncounter);
                await _unitOfWork.SaveAsync();

                return patientEncounter;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PatientMasterVisit> AddPatientMasterVisit(int patientId, int serviceAreaId, DateTime encounterDate, int userId)
        {
            try
            {
                PatientMasterVisit patientMasterVisit = new PatientMasterVisit()
                {
                    PatientId = patientId,
                    ServiceId = serviceAreaId,
                    Start = encounterDate,
                    Active = true,
                    CreateDate = DateTime.Now,
                    DeleteFlag = false,
                    VisitDate = encounterDate,
                    CreatedBy = userId,
                    VisitType = 0
                };

                await _unitOfWork.Repository<PatientMasterVisit>().AddAsync(patientMasterVisit);
                await _unitOfWork.SaveAsync();

                return patientMasterVisit;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}