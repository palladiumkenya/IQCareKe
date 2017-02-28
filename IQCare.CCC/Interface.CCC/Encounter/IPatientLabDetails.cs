using System;
using System.Collections.Generic;
using System.Linq;
using Entities.CCC.Encounter;

namespace Interface.CCC.Encounter
{
    public interface IPatientLabDetails
    {
        int AddPatientLabOrder(LabDetailsEntity labDetailsEntity);
        int UpdatePatientLabOrder(LabDetailsEntity labDetailsEntity);
        int DeletePatientLabOrder(int id);
        List<LabDetailsEntity> GetPatientCurrentLabDetails(int patientId, DateTime visitDate);
        List<LabDetailsEntity> GetPatientLabDetailsAll(int patientId);
    }
}
