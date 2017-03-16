using System;
using System.Collections.Generic;
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
       int PatientMasterVisitCheckin(int patientId,PatientMasterVisit patientMasterVisit);
       int PatientMasterVisitCheckout(int patientId,PatientMasterVisit patientMasterVisit);
       int PatientMasterVisitCheckout(int patientId, int masterVisitId, int visitSchedule, int visitBy, int visitType, DateTime visitDate);
       void PatientMasterVisitAutoClosure(int patientId);

   }
}
