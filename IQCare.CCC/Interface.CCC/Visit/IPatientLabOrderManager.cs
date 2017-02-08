using System;
using System.Collections.Generic;
using Entities.CCC.Visit;

namespace Interface.CCC.Visit
{
    public interface IPatientLabOrderManager
    {
        int AddPatientLabOrder(PatientLabTracker patientLabTracker);
        int UpdatePatientLabOrder(PatientLabTracker patientLabTracker);
        int DeletePatientLabOrder (int id);
        List<PatientLabTracker> GetPatientCurrentLabOrders(int patientId, DateTime visitDate);
        List<PatientLabTracker> GetPatientLabOrdersAll(int patientId);
    }
}
