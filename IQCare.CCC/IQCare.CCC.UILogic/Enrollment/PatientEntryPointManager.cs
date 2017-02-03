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

        public int addPatientEntryPoint(PatientEntryPoint patientEntryPoint)
        {
            int returnValue;
            try
            {
                returnValue = _mgr.AddPatientEntryPoint(patientEntryPoint);
                return returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
