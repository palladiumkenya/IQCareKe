using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IQCare.PMTCT.Core.Models;

namespace IQCare.PMTCT.Services.Interface
{
    public interface IPatientProfileService
    {
        Task<PatientProfile> AddPatientProfile(PatientProfile patientProfile);
        Task<PatientProfile> EditPatientProfile(PatientProfile patientProfile);
        Task<PatientProfile> GetPatientProfile(int patientId);
        Task<List<PatientProfile>> GetAllPatientProfileAsync(int patientId);
    }
}
