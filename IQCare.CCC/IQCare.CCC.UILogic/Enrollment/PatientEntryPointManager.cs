using Application.Presentation;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic.Enrollment
{
    public class PatientEntryPointManager
    {
        IPatientEntryPointManager _mgr = (IPatientEntryPointManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Enrollment.BPatientEntryPoint, BusinessProcess.CCC");

        public int addPatientEntryPoint(int patientId, int entryPointId, int userId)
        {
            int returnValue;
            try
            {
                PatientEntryPoint patientEntryPoint = new PatientEntryPoint
                {
                    PatientId = patientId,
                    ServiceAreaId = 1,
                    EntryPointId = entryPointId,
                    CreatedBy = userId,
                    CreateDate = DateTime.Now,
                    DeleteFlag = false
                };

                returnValue = _mgr.AddPatientEntryPoint(patientEntryPoint);
                return returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdatePatientEntryPoint(PatientEntryPoint patientEntryPoint)
        {
            try
            {
                return _mgr.UpdatePatientEntryPoint(patientEntryPoint);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PatientEntryPoint> GetPatientEntryPoints(int patientId)
        {
            try
            {
                return _mgr.GetPatientEntryPoints(patientId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PatientEntryPoint> GetPatientEntryPoints(int patientId, int serviceAreaId)
        {
            try
            {
                return _mgr.GetPatientEntryPoints(patientId, serviceAreaId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
