using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.PatientCore;

namespace Interface.CCC
{
   public interface IPatientMaritalStatusManager
   {
       int AddPatientMaritalStatus(PatientMaritalStatus patientMarital);
       int UpdatePatientMaritalStatus(PatientMaritalStatus patientMarital);
       int DeletePatientMaritalStatus(int id);
       List<PatientMaritalStatus> GetAllMaritalStatuses(int personId);
   }
}
