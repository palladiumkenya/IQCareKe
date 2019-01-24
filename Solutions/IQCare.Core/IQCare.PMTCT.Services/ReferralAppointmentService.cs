using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services.Interface;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.PMTCT.Services
{
   public class ReferralAppointmentService:IReferralAppointmentService
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public ReferralAppointmentService(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddPatientAppointment(PatientAppointment patientAppointment)
        {
            try
            {
                await _unitOfWork.Repository<PatientAppointment>().AddAsync(patientAppointment);
                await _unitOfWork.SaveAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<int> AddPatientReferral(PatientReferral patientReferral)
        {
            try
            {
                await _unitOfWork.Repository<PatientReferral>().AddAsync(patientReferral);
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
