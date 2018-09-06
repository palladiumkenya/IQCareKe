using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.Services
{
   public class PatientProfileSerivce : IPatientProfileService
   {
       private readonly IPmtctUnitOfWork _unitOfWork;
        public async Task<PatientProfile> AddPatientProfile(PatientProfile patientProfile)
        {
            try
            {
                await _unitOfWork.Repository<PatientProfile>().AddAsync(patientProfile);
                await _unitOfWork.SaveAsync();
                return patientProfile;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<PatientProfile> EditPatientProfile(PatientProfile patientProfile)
        {
            try
            {
                _unitOfWork.Repository<PatientProfile>().Update(patientProfile);
                await _unitOfWork.SaveAsync();
                return patientProfile;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<PatientProfile> GetPatientProfile(int patientId)
        {
            try
            {
                PatientProfile patientProfile = await _unitOfWork.Repository<PatientProfile>()
                    .Get(x => x.PatientId == patientId &&  x.DeleteFlag==0).FirstOrDefaultAsync();
                return patientProfile;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<List<PatientProfile>> GetAllPatientProfileAsync(int patientId)
        {
            try
            {
                List<PatientProfile> patientProfile = await _unitOfWork.Repository<PatientProfile>()
                    .Get(x => x.PatientId == patientId && x.DeleteFlag == 0).ToListAsync();
                return patientProfile;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }
    }
}
