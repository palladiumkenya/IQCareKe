
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services.Interface;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using PatientClinicalNotes = IQCare.PMTCT.Core.Models.PatientClinicalNotes;
using PatientScreening = IQCare.PMTCT.Core.Models.PatientScreening;

namespace IQCare.PMTCT.Services
{
    public  class ClientMonitoringServices :IClientMonitoringService
    {
       private readonly IPmtctUnitOfWork _unitOfWork;

        public ClientMonitoringServices(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddPatientClinicalNotes(PatientClinicalNotes patientClinicalNotes)
        {
            try
            {
                await _unitOfWork.Repository<PatientClinicalNotes>().AddAsync(patientClinicalNotes);
                await _unitOfWork.SaveAsync();
                return patientClinicalNotes.Id;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

        public async Task<int> AddPatientScreening(PatientScreening patientScreening)
        {
            try
            {
                await _unitOfWork.Repository<PatientScreening>().AddAsync(patientScreening);
                await _unitOfWork.SaveAsync();

                return patientScreening.Id;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

        public PatientScreening GetPatientScreening(int patientId, int patientMasterVisitId, int screeningId)
        {
            try
            {
                PatientScreening patientScreening = _unitOfWork.Repository<PatientScreening>().Get(x =>
                    x.PatientId == patientId && x.PatientMasterVisitId == patientMasterVisitId &&
                    x.ScreeningTypeId == screeningId).FirstOrDefault();
                return patientScreening;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

        public async Task<int> EditPatientScreening(PatientScreening patientScreening)
        {
            try
            {
                 _unitOfWork.Repository<PatientScreening>().Update(patientScreening);
                await _unitOfWork.SaveAsync();

                return patientScreening.Id;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }



        public async Task<int>  AddPatientWhoStage(PatientWhoStage patientWHOStage)
        {
            try
            {
                await  _unitOfWork.Repository<PatientWhoStage>().AddAsync(patientWHOStage);
                await _unitOfWork.SaveAsync();
                return patientWHOStage.Id;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

        public PatientWhoStage GetPatientWhoStage(int patientId, int PatientMasterVisitId)
        {
            try
            {
                PatientWhoStage patientWhoStage = _unitOfWork.Repository<PatientWhoStage>()
                    .Get(x => x.PatientId == patientId && x.PatientMasterVisitId == PatientMasterVisitId)
                    .FirstOrDefault();
                return patientWhoStage;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

        public async Task<int> EditPatientWhoStage(PatientWhoStage patientWHOStage)
        {
            try
            {
                _unitOfWork.Repository<PatientWhoStage>().Update(patientWHOStage);
                await _unitOfWork.SaveAsync();
                return patientWHOStage.Id;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

    }
}
