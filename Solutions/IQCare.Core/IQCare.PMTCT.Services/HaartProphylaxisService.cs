using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services.Interface;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQCare.PMTCT.Services
{
    public class HaartProphylaxisService : IHaartProphylaxisService
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public HaartProphylaxisService(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddPatientChronicIllness(List<PatientChronicIllness> patientChronicIllnesses)
        {
            try
            {
                await _unitOfWork.Repository<PatientChronicIllness>().AddRangeAsync(patientChronicIllnesses);
                await _unitOfWork.SaveAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<int> AddPatientDrugAdministration(List<PatientDrugAdministration> patientDrugAdministrations)
        {
            try
            {
                await _unitOfWork.Repository<PatientDrugAdministration>().AddRangeAsync(patientDrugAdministrations);
                await _unitOfWork.SaveAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }
    }
}
