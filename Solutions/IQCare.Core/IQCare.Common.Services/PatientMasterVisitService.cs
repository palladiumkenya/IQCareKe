using System;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Serilog;

namespace IQCare.Common.Services
{
    public class PatientMasterVisitService
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;

        public PatientMasterVisitService(ICommonUnitOfWork commonUnitOfWork)
        {
            this._commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<PatientMasterVisit> Add(int patientId, int serviceId, DateTime start, int createdBy, DateTime? visitDate, DateTime? end, int? visitScheduled, int? visitBy, int? visitType, int? status)
        {
            try

            {
                PatientMasterVisit patientMasterVisit = new PatientMasterVisit()
                {
                    PatientId = patientId,
                    ServiceId = serviceId,
                    Start = start,
                    Active = false,
                    End = end,
                    VisitScheduled = visitScheduled,
                    VisitBy = visitBy,
                    VisitType = visitType,
                    VisitDate = visitDate,
                    Status = status,
                    CreateDate = DateTime.Now,
                    CreatedBy = createdBy,
                    DeleteFlag = false
                };

                await _commonUnitOfWork.Repository<PatientMasterVisit>().AddAsync(patientMasterVisit);
                await _commonUnitOfWork.SaveAsync();

                return patientMasterVisit;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<PatientMasterVisit> Update(PatientMasterVisit patientMasterVisit)
        {
            try
            {
                _commonUnitOfWork.Repository<PatientMasterVisit>().Update(patientMasterVisit);
                await _commonUnitOfWork.SaveAsync();

                return patientMasterVisit;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }
    }
}