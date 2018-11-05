using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQCare.PMTCT.Infrastructure;

namespace IQCare.PMTCT.Services
{
    public class VisitDetailsService
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public VisitDetailsService(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PatientPregnancy> AddPatientPregnancy(PatientPregnancy patientPregnancy)
        {
            try
            {
                await _unitOfWork.Repository<PatientPregnancy>().AddAsync(patientPregnancy);
                await _unitOfWork.SaveAsync();
                return patientPregnancy;
            }catch(Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<PatientPregnancy> EditPatientPreganancy(PatientPregnancy patientPregnancy)
        {
            try
            {
                PatientPregnancy _patientPregnancy = _unitOfWork.Repository<PatientPregnancy>().FindById(patientPregnancy.Id);
                _patientPregnancy.Lmp = patientPregnancy.Lmp;
                _patientPregnancy.Edd = patientPregnancy.Edd;
                _patientPregnancy.DateOfOutcome = patientPregnancy.DateOfOutcome;
                _patientPregnancy.Parity = patientPregnancy.Parity;
                _patientPregnancy.Gravidae = patientPregnancy.Parity;

                _unitOfWork.Repository<PatientPregnancy>().Update(_patientPregnancy);
                await _unitOfWork.SaveAsync();
                return patientPregnancy;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }


        public async Task<PatientPregnancy> GetPatientPregnancy(int PatientId)
        {
            try
            {
                PatientPregnancy _patientPregnancy = await _unitOfWork.Repository<PatientPregnancy>().Get(x => x.PatientId == PatientId).FirstOrDefaultAsync();
                return _patientPregnancy;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<PatientEducation> AddPatientEducation(PatientEducation patientEducation)
        {
            try
            {
                await _unitOfWork.Repository<PatientEducation>().AddAsync(patientEducation);
                await _unitOfWork.SaveAsync();
                return patientEducation;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<PatientEducation> EditPatientEducation(PatientEducation patientEducation)
        {
            try
            {
                PatientEducation _patientEducation = _unitOfWork.Repository<PatientEducation>().FindById(patientEducation.Id);
                _patientEducation.CounsellingDate = patientEducation.CounsellingDate;
                _patientEducation.CounsellingTopicId = patientEducation.CounsellingTopicId;

                _unitOfWork.Repository<PatientEducation>().Update(patientEducation);
                await _unitOfWork.SaveAsync();
                return patientEducation;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<List<PatientEducation>> GetPatientEducation(int PatientId)
        {
            try
            {
               List<PatientEducation>  patientEducation = await _unitOfWork.Repository<PatientEducation>().Get(x => x.PatientId == PatientId).ToListAsync();
                return patientEducation;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

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
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<PatientProfile> EditPatientProfile(PatientProfile patientProfile)
        {
            try
            {
                var _patientProfile = _unitOfWork.Repository<PatientProfile>().FindById(patientProfile.Id);
                PatientProfile patientProfile_ = new PatientProfile()
                {
                    PregnancyId = patientProfile.PregnancyId,
                    VisitNumber = patientProfile.VisitNumber,
                    VisitType = patientProfile.VisitType,
                   // CounselledOn = patientProfile.CounselledOn,
                    TreatedForSyphilis = patientProfile.TreatedForSyphilis
                };
                _unitOfWork.Repository<PatientProfile>().Update(patientProfile_);
                await _unitOfWork.SaveAsync();
                return patientProfile;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public  async Task<List<PatientProfile>> GetPatientProfile(int PatientId)
        {
            try
            {
                var profile = await _unitOfWork.Repository<PatientProfile>().Get(x => x.PatientId == PatientId && !x.DeleteFlag).ToListAsync();
                return profile;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

    }

}