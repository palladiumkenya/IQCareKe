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
   public class PatientOIManager
    {
        IPatientOIManager _mgr = (IPatientOIManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientOIManager, BusinessProcess.CCC");

        public PatientOI addPatientOI(int patientId, int patientMasterVisitId, int OI, int User, DateTime? current)
        {
            try
            {
                PatientOI patientOI = new PatientOI()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    OIId = OI,
                    Current=current,
                    DeleteFlag = false,
                    CreateDate = DateTime.Now,
                    CreatedBy=User

                };

                return _mgr.addPatientOI(patientOI);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PatientOI> GetPatientsOI(int patientId, int patientMasterVisitId)
        {
            try
            {
                return _mgr.GetPatientOIs(patientId, patientMasterVisitId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public PatientOI GetPatientOI(int patientId, int patientMasterVisitId,int OI)
        {
            try
            {
                return _mgr.GetPatientOI(patientId, patientMasterVisitId,OI);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PatientOI UpdatePatientOI(PatientOI patientOI)
        {
            try
            {
                return _mgr.UpdatePatientOI(patientOI);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PatientOI GetPatientOIById(int entityId)
        {
            try
            {
                return _mgr.GetPatientOIbyId(entityId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<PatientOI> GetPatientOIByPatient(int patientid)
        {
            try
            {
                return _mgr.GetOIListByPatient(patientid);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
