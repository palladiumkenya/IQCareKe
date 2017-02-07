using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Visit;

namespace Interface.CCC.Visit
{
   public interface IPatientMasterVisitManager
   {
       int AddPatientmasterVisit(PatientMasterVisit patientMasterVisit);
       int UpdatePatientMasterVisit(PatientMasterVisit patientMasterVisit);
       int DeletePatientVisit(int id);
       List<PatientMasterVisit> GetPatientVisits(int patientId);
       List<PatientMasterVisit> GetPatientCurrentVisit(int patientId, DateTime visitDate);
       
   }
}
