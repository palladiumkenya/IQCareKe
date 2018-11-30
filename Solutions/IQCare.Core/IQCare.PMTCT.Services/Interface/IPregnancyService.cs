using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IQCare.PMTCT.Core.Models;

namespace IQCare.PMTCT.Services.Interface
{
  public  interface IPregnancyService
  {
      Task<PatientPregnancy> AddPatientPregnancy(PatientPregnancy patientPregnancy);
      Task<PatientPregnancy> EditPatientPregnancy(PatientPregnancy patientPregnancy);
      PatientPregnancy GetActivePregnancy(int patientId);
      Task<List<PatientPregnancy>> GetPreviousPregnancies(int patientId);
  }
}
