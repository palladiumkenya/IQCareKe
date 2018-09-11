
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services.Interface;
using Serilog;
using System;
using System.Threading.Tasks;

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

        public async Task<int>  AddPatientWhoStage(PatientWHOStage patientWHOStage)
        {
            try
            {
                await  _unitOfWork.Repository<PatientWHOStage>().AddAsync(patientWHOStage);
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
