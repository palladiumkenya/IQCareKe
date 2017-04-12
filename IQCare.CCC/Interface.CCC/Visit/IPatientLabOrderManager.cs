using System;
using System.Collections.Generic;
using Entities.CCC.Visit;
using Entities.CCC.Encounter;

namespace Interface.CCC.Visit
{
    public interface IPatientLabOrderManager
    {
        int AddPatientLabTracker(PatientLabTracker patientLabTracker);      

        int AddPatientLabOrder(LabOrderEntity labOrderEntity);
        int UpdatePatientLabOrder(PatientLabTracker patientLabTracker);
        int DeletePatientLabOrder (int id);
        List<PatientLabTracker> GetPatientCurrentLabOrders(int patientId, DateTime visitDate);
        List<PatientLabTracker> GetPatientLabOrdersAll(int patientId);
        LabOrderEntity GetPatientLabOrder(int Ptn_pk);
        List<LabResultsEntity> GetPatientVL(int patientId);
        List<LabOrderEntity> GetVlPendingCount(int facilityId);
        List<LabOrderEntity> GetVlCompleteCount(int facilityId);
    }
}
