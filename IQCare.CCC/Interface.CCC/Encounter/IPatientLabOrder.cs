using System;
using System.Collections.Generic;
using System.Linq;
using Entities.CCC.Encounter;

namespace Interface.CCC.Encounter
{
    public interface IPatientLabOrder
    {
        int AddPatientLabOrder(LabOrderEntity labOrderEntity);    
        int UpdatePatientLabOrder(LabOrderEntity labOrderEntity);
        int DeletePatientLabOrder(int id);
        List<LabOrderEntity> GetPatientCurrentLabOrders(int patientId, DateTime visitDate);
        List<LabOrderEntity> GetPatientLabOrdersAll(int patientId);
      
    }
}
