using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IQCare.PMTCT.Core.Models;

namespace IQCare.PMTCT.Services.Interface
{
   public interface IPatientPreventiveService
   {
       Task<int> AddPatientParterTesting(PatientPartnerTesting patientPartnerTesting);
       Task<int> AddPatientPreventiveService(List<PreventiveService> preventiveServices);
       Task<int> AddPatientAppointment(PatientAppointment patientAppointment);

   }
}
