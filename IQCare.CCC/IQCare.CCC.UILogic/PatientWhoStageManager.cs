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
    public class PatientWhoStageManager
    {
        IPatientWhoStageManager _mgr = (IPatientWhoStageManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientWhoStageManager, BusinessProcess.CCC");

        public int addPatientWhoStage(int patientId, int patientMasterVisitId, int whoStage)
        {
            try
            {
                PatientWhoStage patientWhoStage = new PatientWhoStage()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    WHOStage = whoStage
                };

                return _mgr.addPatientWhoStage(patientWhoStage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PatientWhoStage GetPatientWhoStage(int patientId, int patientMasterVisitId)
        {
            try
            {
                return _mgr.GetPatientWhoStage(patientId, patientMasterVisitId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int UpdatePatientWhoStage(PatientWhoStage patientWhoStage)
        {
            try
            {
                return _mgr.UpdatePatientWhoStage(patientWhoStage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PatientWhoStage GetWhoStageById(int entityId)
        {
            try
            {
                return _mgr.GetWhoStageById(entityId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PatientWhoStage> WhoStagelistByPatientId(int patientId)
        {
            try
            {
                return _mgr.GetWhoStageListByPatient(patientId);
            }
            catch(Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}
