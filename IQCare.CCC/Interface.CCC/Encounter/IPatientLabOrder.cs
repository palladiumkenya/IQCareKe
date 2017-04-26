using System;
using System.Collections.Generic;
using System.Linq;
using Entities.CCC.Encounter;
using Entities.CCC.Visit;

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
