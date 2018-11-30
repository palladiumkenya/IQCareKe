using System;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using Serilog;

namespace IQCare.PMTCT.Services
{
    public class PatientProfileService
    {
        public readonly IPmtctUnitOfWork _PmtctUnitOfWork;

        public PatientProfileService(IPmtctUnitOfWork pmtctUnitOfWork)
        {
            _PmtctUnitOfWork = pmtctUnitOfWork;
        }

        public async Task<PatientProfile> AddPatientProfile(PatientProfile patientProfile)
        {
            try
            {
                PatientProfile patientProfileData = new PatientProfile()
                {
                    PatientMasterVisitId = patientProfile.PatientMasterVisitId,
                    PatientId = patientProfile.PatientId,
                    PregnancyId = patientProfile.PregnancyId,
                    VisitType = patientProfile.VisitType,
                    VisitNumber = patientProfile.VisitNumber
                };
                await _PmtctUnitOfWork.Repository<PatientProfile>().AddAsync(patientProfileData);
                await _PmtctUnitOfWork.SaveAsync();

                return patientProfileData;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

        public async Task<PatientProfile> UpdatePatientProfile(PatientProfile patientProfile)
        {
            try
            {
                PatientProfile patientProfileData =
                    _PmtctUnitOfWork.Repository<PatientProfile>().FindById(patientProfile.Id);
                if (null!=patientProfileData)
                {
                    patientProfileData.PatientMasterVisitId = patientProfileData.PatientMasterVisitId;
                    patientProfileData.PregnancyId = patientProfileData.PregnancyId;
                    patientProfileData.TreatedForSyphilis = patientProfileData.TreatedForSyphilis;
                    patientProfileData.VisitType = patientProfileData.VisitType;
                    patientProfileData.VisitNumber = patientProfileData.VisitNumber;

                }
                _PmtctUnitOfWork.Repository<PatientProfile>().Update(patientProfileData);
                await _PmtctUnitOfWork.SaveAsync();
                return patientProfileData;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

        public async Task<PatientProfile> DeletePatientProfile(int Id)
        {
            try
            {
                PatientProfile patientProfile = _PmtctUnitOfWork.Repository<PatientProfile>().FindById(Id);
                if(null!=patientProfile)
                {
                    patientProfile.DeleteFlag = false;
                }
                _PmtctUnitOfWork.Repository<PatientProfile>().Update(patientProfile);
                await _PmtctUnitOfWork.SaveAsync();

                return patientProfile;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

    }
}