using Entities.CCC.Encounter;
using Entities.CCC.Visit;
using System;
using System.Collections.Generic;

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
        List<LabOrderEntity> GetPatientLabOrdersByDate(int patientId, DateTime visitDate);
        List<LabDetailsEntity> GetPatientLabDetailsByDate(int labOrderId, DateTime visitDate);
        List<LabDetailsEntity> GetPatientLabDetailsByLabOrderId(int labOrderId);
        List<PatientLabTracker> GetAllPatientVLs(int patientId);
        PatientLabTracker GetPatientLastVL(int patientId);

        LabOrderEntity GetLabOrderById(int labOrderId);
        void EditPatientLabOrder(LabOrderEntity labOrder);
    }
}
