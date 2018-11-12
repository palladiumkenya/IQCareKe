using Entities.CCC.Encounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.CCC.Encounter
{
   public  interface IPatientHighRiskManager
    {
        PatientHighRisk addPatientHighRisk(PatientHighRisk patienthighrisk);
        PatientHighRisk GetPatientHighRisk(int patientId, int patientMasterVisitId, int partnerId);
        List<PatientHighRisk> GetPatientHighRisksList(int patientId, int patientMasterVisitId, int partnerId);
        PatientHighRisk GetPatientHighRisks(int patientId, int patientMasterVisitId, int partnerId,int HighRisk);
        PatientHighRisk GetPatientHighRiskbyId(int entityId);

        PatientHighRisk UpdatePatientHighRisk(PatientHighRisk patienthighrisk);

    }
}
