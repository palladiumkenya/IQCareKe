using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services.Interface;
using Serilog;

namespace IQCare.PMTCT.Services
{
    public class PatientPreventiveService : IPatientPreventiveService
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public PatientPreventiveService(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddPatientAppointment(PatientAppointment patientAppointment)
        {
            try
            {
                await _unitOfWork.Repository<PatientAppointment>().AddAsync(patientAppointment);
                await _unitOfWork.SaveAsync();

                return patientAppointment.Id;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<int> AddPatientParterTesting(PatientPartnerTesting patientPartnerTesting)
        {
            try
            {
                await _unitOfWork.Repository<PatientPartnerTesting>().AddAsync(patientPartnerTesting);
                await _unitOfWork.SaveAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<int> AddPatientPreventiveService(List<PreventiveService> preventiveServices)
        {
            try
            {
                await _unitOfWork.Repository<PreventiveService>().AddRangeAsync(preventiveServices);
                await _unitOfWork.SaveAsync();
                return  1;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }
    }
}
