using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;
namespace IQCare.CCC.UILogic
{
    public class PatientHighRiskManager
    {
        IPatientHighRiskManager  _mgr = (IPatientHighRiskManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientHighRiskManager, BusinessProcess.CCC");

        public PatientHighRisk addPatientHighRisk(PatientHighRisk patientHighRisk)
        {
            return _mgr.addPatientHighRisk(patientHighRisk);
        }

        public PatientHighRisk GetPatientHighRisks(int patientId, int patientMasterVisitId, int partnerId, int HighRisk)
        {
            return _mgr.GetPatientHighRisks(patientId, patientMasterVisitId,partnerId,HighRisk);
        }

        public List<PatientHighRisk> GetPatientHighRiskList(int patientid,int patientmastervisitid,int partnerid)
        {
            return _mgr.GetPatientHighRisksList(patientid, patientmastervisitid, partnerid);
        }
        public PatientHighRisk GetPatientHighRiskbyId(int  entityId)
        {
            return _mgr.GetPatientHighRiskbyId(entityId);
        }

        public PatientHighRisk UpdatePatientHighRisk(PatientHighRisk pat)
        {
            return _mgr.UpdatePatientHighRisk(pat);
        }
    }
}
