using IQCare.Common.Core.Models;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IQCare.PMTCT.Services
{
    public class PatientEducationExaminationService
    {
        private readonly IPmtctUnitOfWork _unitOfWork;
        private readonly ICommonUnitOfWork _commonUnitOfWork;

        public PatientEducationExaminationService(IPmtctUnitOfWork unitOfWork,ICommonUnitOfWork commonUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _commonUnitOfWork = commonUnitOfWork;
        }

        public async Task<int> AddPatientPhysicalExamination(PatientPhysicalExamination patientPhysicalExamination)
        {
            try
            {
                PatientPhysicalExamination data = new PatientPhysicalExamination()
                {
                    PatientId = patientPhysicalExamination.PatientId,
                    PatientMasterVisitId = patientPhysicalExamination.PatientMasterVisitId,
                    ExamId = patientPhysicalExamination.ExamId,
                    ExaminationTypeId = patientPhysicalExamination.ExaminationTypeId,
                    FindingId = patientPhysicalExamination.FindingId,
                    FindingsNotes = patientPhysicalExamination.FindingsNotes
                };
                await _commonUnitOfWork.Repository<PatientPhysicalExamination>().AddAsync(data);
                 await _commonUnitOfWork.SaveAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

        public async Task<PatientPhysicalExamination> GetPatientPhysicalExamination(int patientId,int examId)
        {
            try
            {
                var result = await _commonUnitOfWork.Repository<PatientPhysicalExamination>().FindAsync(x => x.ExamId == examId && x.PatientId == patientId);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

        public async Task<int> AddPatientEducation(List<PatientEducation> patientEducation)
        {
            try
            {
                await _unitOfWork.Repository<PatientEducation>().AddRangeAsync(patientEducation);
                await _unitOfWork.SaveAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

        public async Task<List<PatientEducation>> GetPatientEducation(int patientId)
        {
            try
            {
                var result =await _unitOfWork.Repository<PatientEducation>().Get(x => x.PatientId == patientId).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }
    }
}
