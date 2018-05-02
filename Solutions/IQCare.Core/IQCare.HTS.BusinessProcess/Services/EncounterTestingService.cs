using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.HTS.Core.Model;

namespace IQCare.HTS.BusinessProcess.Services
{
    public class EncounterTestingService
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public EncounterTestingService(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
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
                    await _unitOfWork.Repository<ClientDisability>().AddRangeAsync(clientDisabilities);
                    await _unitOfWork.SaveAsync();
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

                await _unitOfWork.Repository<HtsEncounter>().AddAsync(htsEncounter);
                await _unitOfWork.SaveAsync();

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