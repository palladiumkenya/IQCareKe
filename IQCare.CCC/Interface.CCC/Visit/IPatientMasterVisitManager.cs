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
       int PatientMasterVisitCheckout(int masterVisitId);
       void PatientMasterVisitAutoClosure(int patientId);
       DateTime GetPatientLastVisitDate(int patientId);
       List<PatientMasterVisit> GetByDate(DateTime date);
       List<PatientMasterVisit> GetNonEnrollmentVisits(int patientId, int visitType);
       PatientMasterVisit GetLastPatientVisit(int patientId);
       PatientMasterVisit GetVisitById(int id);
        List<PatientMasterVisit> GetPatientMasterVisitBasedonVisitDate(int patientId, DateTime visitDate);

        List<PatientMasterVisit> GetVisitDateByMasterVisitId(int patientId, int PatientMasterVisitId);
   }
}
