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
        //int AddPatientLabDetails(LabDetailsEntity labDetailsEntity);
        int UpdatePatientLabOrder(PatientLabTracker patientLabTracker);
        int DeletePatientLabOrder (int id);
        List<PatientLabTracker> GetPatientCurrentLabOrders(int patientId, DateTime visitDate);
        List<PatientLabTracker> GetPatientLabOrdersAll(int patientId);
    }
}
