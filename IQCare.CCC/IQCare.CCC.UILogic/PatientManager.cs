using Application.Presentation;
using Interface.CCC.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic
{
    public class PatientManager
    {
        IPatientManager _mgr = (IPatientManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Patient.BPatient, BusinessProcess.CCC");

        public int addPatient(Entities.CCC.Enrollment.PatientEntity patient)
        {
            int returnValue;

            try
            {
                returnValue = _mgr.AddPatient(patient);
                return returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
