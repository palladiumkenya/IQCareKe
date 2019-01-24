
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQCare.Common.Infrastructure;
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

        public async Task<int> AddPatientPhysicalExamination(PhysicalExamination physicalExamination)
        {
            try
            {
                PhysicalExamination data = new PhysicalExamination()
                {
                    PatientId = physicalExamination.PatientId,
                    PatientMasterVisitId = physicalExamination.PatientMasterVisitId,
                    ExamId = physicalExamination.ExamId,
                    ExaminationTypeId = physicalExamination.ExaminationTypeId,
                    FindingId = physicalExamination.FindingId,
                    FindingsNotes = physicalExamination.FindingsNotes,
                    CreateDate = DateTime.Now
                };
                await _unitOfWork.Repository<PhysicalExamination>().AddAsync(data);
                await _unitOfWork.SaveAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw;
            }
        }

        public async Task<PhysicalExamination> GetPatientPhysicalExamination(int patientId,int examId)
        {
            try
            {
                var result = await _unitOfWork.Repository<PhysicalExamination>().FindAsync(x => x.ExamId == examId && x.PatientId == patientId);
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
