using System;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Serilog;

namespace IQCare.Common.Services
{
    public class PatientEncounterService
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;

        public PatientEncounterService(ICommonUnitOfWork commonUnitOfWork)
        {
           this. _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork)); ;
        }

        public async Task<PatientEncounter> Add(int patientId,int encounterTypeId,int  patientMasterVisitId, DateTime encounterStartTime,DateTime encounterEndTime,int serviceAreaId)
        {
            try
            {
                PatientEncounter patientEncounter=new PatientEncounter()
                {
                    PatientId = patientId,
                    EncounterTypeId = encounterTypeId,
                    PatientMasterVisitId = patientMasterVisitId,
                    EncounterStartTime = encounterStartTime,
                    EncounterEndTime = encounterEndTime,
                    ServiceAreaId = serviceAreaId
                };

                await _commonUnitOfWork.Repository<PatientEncounter>().AddAsync(patientEncounter);
                await _commonUnitOfWork.SaveAsync();

                return patientEncounter;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<PatientEncounter> Update(PatientEncounter patientEncounter)
        {
            try
            {
                _commonUnitOfWork.Repository<PatientEncounter>().Update(patientEncounter);
                await _commonUnitOfWork.SaveAsync();

                return patientEncounter;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }
    }
}