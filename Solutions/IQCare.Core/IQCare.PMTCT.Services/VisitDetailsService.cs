using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQCare.PMTCT.Services
{
    public class VisitDetailsService
    {
        public IPmtctUnitOfWork PmtctUnitOfWork;

        public VisitDetailsService(IPmtctUnitOfWork pmtctUnitOfWork)
        {
            PmtctUnitOfWork = pmtctUnitOfWork;
        }

        public async Task<PatientPregnancy> AddPatientPregnancy(PatientPregnancy patientPregnancy)
        {
            try
            {
                await PmtctUnitOfWork.Repository<PatientPregnancy>().AddAsync(patientPregnancy);
                await PmtctUnitOfWork.SaveAsync();
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
                PatientPregnancy _patientPregnancy = PmtctUnitOfWork.Repository<PatientPregnancy>().FindById(patientPregnancy.Id);
                _patientPregnancy.Lmp = patientPregnancy.Lmp;
                _patientPregnancy.Edd = patientPregnancy.Edd;
                _patientPregnancy.DateOfOutcome = patientPregnancy.DateOfOutcome;
                _patientPregnancy.Parity = patientPregnancy.Parity;
                _patientPregnancy.Gravidae = patientPregnancy.Parity;
                
                 PmtctUnitOfWork.Repository<PatientPregnancy>().Update(_patientPregnancy);
                await PmtctUnitOfWork.SaveAsync();
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
                PatientPregnancy _patientPregnancy = await PmtctUnitOfWork.Repository<PatientPregnancy>().Get(x => x.PatientId == PatientId).FirstOrDefaultAsync();
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
                await PmtctUnitOfWork.Repository<PatientEducation>().AddAsync(patientEducation);
                await PmtctUnitOfWork.SaveAsync();
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
                PatientEducation _patientEducation = PmtctUnitOfWork.Repository<PatientEducation>().FindById(patientEducation.Id);
                _patientEducation.CounsellingDate = patientEducation.CounsellingDate;
                _patientEducation.CounsellingTopicId = patientEducation.CounsellingTopicId;
                
                PmtctUnitOfWork.Repository<PatientEducation>().Update(patientEducation);
                await PmtctUnitOfWork.SaveAsync();
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
               List<PatientEducation>  patientEducation = await PmtctUnitOfWork.Repository<PatientEducation>().Get(x => x.PatientId == PatientId).ToListAsync();
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
              await  PmtctUnitOfWork.Repository<PatientProfile>().AddAsync(patientProfile);
                await PmtctUnitOfWork.SaveAsync();
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
                var _patientProfile = PmtctUnitOfWork.Repository<PatientProfile>().FindById(patientProfile.Id);
                PatientProfile patientProfile_ = new PatientProfile()
                {
                    PregnancyId = patientProfile.PregnancyId,
                    VisitNumber = patientProfile.VisitNumber,
                    VisitType = patientProfile.VisitType,
                    CounselledOn = patientProfile.CounselledOn,
                    TreatedForSyphilis = patientProfile.TreatedForSyphilis
                };
                PmtctUnitOfWork.Repository<PatientProfile>().Update(patientProfile_);
                await PmtctUnitOfWork.SaveAsync();
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
                var profile = await PmtctUnitOfWork.Repository<PatientProfile>().Get(x => x.PatientId == PatientId && x.DeleteFlag == 0).ToListAsync();
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