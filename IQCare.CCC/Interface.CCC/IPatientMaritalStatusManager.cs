using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.PatientCore;

namespace Interface.CCC
{
   public interface IPatientMaritalStatusManager
   {
       void AddPatientMaritalStatus(PatientMaritalStatus patientMarital);
       void UpdatePatientMaritalStatus(PatientMaritalStatus patientMarital);
       void DeletePatientMaritalStatus(int id);
       List<PatientMaritalStatus> GetAllMaritalStatuses(int personId);
   }
}
