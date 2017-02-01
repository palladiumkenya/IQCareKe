using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.PatientCore;

namespace Interface.CCC
{
   public interface IPatientTreatmeantSupporterManager
   {
       int AddPatientTreatmentSupporter(PatientTreatmentSupporter patientTreatmentSupporter);
       int UpdatePatientTreatmentSupporter(PatientTreatmentSupporter patientTreatmentSupporter);
       int DeletePatientTreatmentSupporter(int id);
       List<PatientTreatmentSupporter> GetCurrentTreatmentSupporter(int personId);
       List<PatientTreatmentSupporter> GetAllTreatmentSupporter(int personId);
    }
}
