using System;
using System.Collections.Generic;
using Entities.CCC.Visit;
using Entities.CCC.Encounter;
using Entities.CCC.Lookup;

namespace Interface.CCC.Visit
{
    public interface IPatientLabOrderManager
    {
        int AddPatientLabTracker(PatientLabTracker patientLabTracker);
        int AddPatientLabResults(LabResultsEntity labResultsEntity);
        int AddPatientLabOrder(LabOrderEntity labOrderEntity);
        int UpdatePatientLabOrder(PatientLabTracker patientLabTracker);
        int DeletePatientLabOrder (int id);
        List<PatientLabTracker> GetPatientCurrentLabOrders(int patientId, DateTime visitDate);
        List<PatientLabTracker> GetPatientLabOrdersAll(int patientId);
        PatientLabTracker GetPatientLabTestId(int patientId);
        int  AddLabOrderDetails(LabDetailsEntity labDetailsEntity);       
        List<PatientLabTracker> GetPatientVL(int patientId);
        List<PatientLabTracker> GetPatientVlById(int Id);
        PatientLabTracker GetPatientCurrentviralLoadInfo(int patientId);
      

    }
}
