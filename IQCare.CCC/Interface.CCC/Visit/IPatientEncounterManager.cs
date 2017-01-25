using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Visit;
using Entities.PatientCore;

namespace Interface.CCC.Visit
{
   public interface IPatientEncounterManager
   {
       int AddpatientEncounter(PatientEncounter patientEncounter);
       int UpdatePatientEncounter(PatientEncounter patientEncounter);
       int DeletePatientEncounter(int id);
       List<PatientEncounter> GetPatientCurrentEncounters(int patientId, DateTime visitDate);
       List<PatientEncounter> GetPatientEncounterAll(int patientId);
   }
}
