using IQCare.PMTCT.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.PMTCT.Services.Interface
{
  public  interface IReferralAppointmentService
    {
        Task<int> AddPatientReferral(PatientReferral patientReferral);
        Task<int> AddPatientAppointment(PatientAppointment patientAppointment);
    }
}
