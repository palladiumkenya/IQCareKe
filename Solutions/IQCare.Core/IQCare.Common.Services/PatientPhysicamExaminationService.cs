using IQCare.Common.Infrastructure;
using Serilog;
using System;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.Services
{

    public class PatientPhysicamExaminationService
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public PatientPhysicamExaminationService(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PatientPhysicalExamination> AddPatientPhysicalExamination(PatientPhysicalExamination patientPhysicalExamination)
        {
            try
            {
                await _unitOfWork.Repository<PatientPhysicalExamination>().AddAsync(patientPhysicalExamination);
               await _unitOfWork.SaveAsync();
               return patientPhysicalExamination;
            }
            catch (Exception e)
            {

                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<PatientPhysicalExamination> EditPatientPhysicalExamination(PatientPhysicalExamination patientPhysicalExamination)
        {
            try
            {
                await _unitOfWork.Repository<PatientPhysicalExamination>().AddAsync(patientPhysicalExamination);
                await _unitOfWork.SaveAsync();
                return patientPhysicalExamination;
            }
            catch (Exception e)
            {

                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }


        public async Task<List<PatientPhysicalExamination>> GetPatientPhysicalExamination(int patientId)
        {
            try
            {
                var result= await _unitOfWork.Repository<PatientPhysicalExamination>().Get(x => x.PatientId == patientId).ToListAsync();
                return result;
            }
            catch (Exception e)
            {

                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }


    }
}
