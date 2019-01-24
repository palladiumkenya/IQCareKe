using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.Services
{
    public class PregnancyServices :IPregnancyService
   {
       private readonly IPmtctUnitOfWork _unitOfWork;


       public PregnancyServices(IPmtctUnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork;
       }

        public async Task<PatientPregnancy> AddPatientPregnancy(PatientPregnancy patientPregnancy)
        {
            try
            {
                var _activePregnancy =  GetActivePregnancy(patientPregnancy.PatientId);
                if (_activePregnancy == null)
                {
                    await _unitOfWork.Repository<PatientPregnancy>().AddAsync(patientPregnancy);
                    await _unitOfWork.SaveAsync();
                    return patientPregnancy;
                }
                else
                {
                    return _activePregnancy;
                }
                
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<PatientPregnancy> EditPatientPregnancy(PatientPregnancy patientPregnancy)
        {
            try
            {
                 _unitOfWork.Repository<PatientPregnancy>().Update(patientPregnancy);
                await _unitOfWork.SaveAsync();
                return patientPregnancy;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public  PatientPregnancy GetActivePregnancy(int patientId)
        {
            try
            {
                PatientPregnancy activePregnancy =  _unitOfWork.Repository<PatientPregnancy>()
                    .Get(x => x.PatientId == patientId && !x.Outcome.HasValue).FirstOrDefault();
                return activePregnancy;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<List<PatientPregnancy>> GetPreviousPregnancies(int patientId)
        {
            try
            {
                var activePregnancy = await _unitOfWork.Repository<PatientPregnancy>()
                    .Get(x => x.PatientId == patientId && x.Outcome.HasValue).ToListAsync();
                return activePregnancy;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }
    }
}
