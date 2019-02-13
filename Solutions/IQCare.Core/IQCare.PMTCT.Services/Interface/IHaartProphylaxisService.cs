using IQCare.PMTCT.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.PMTCT.Services.Interface
{
   public interface IHaartProphylaxisService
    {
         Task<int> AddPatientDrugAdministration(List<PatientDrugAdministration> patientDrugAdministrations);
         Task<int> AddPatientChronicIllness(List<PatientChronicIllness> patientChronicIllnesses);

    }
}
