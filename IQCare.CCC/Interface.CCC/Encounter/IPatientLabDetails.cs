using System;
using System.Collections.Generic;
using System.Linq;
using Entities.CCC.Encounter;

namespace Interface.CCC.Encounter
{
    public interface IPatientLabDetails
    {
        int AddLabOrderDetails(LabDetailsEntity labDetailsEntity);
        int UpdatePatientLabDetails(LabDetailsEntity labDetailsEntity);
        int DeletePatientLabDetails(int id);
        List<LabDetailsEntity> GetPatientCurrentLabDetails(int patientId, DateTime visitDate);
        List<LabDetailsEntity> GetPatientLabDetailsAll(int patientId);
    }
}
