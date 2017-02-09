using Application.Presentation;
using Entities.CCC.Encounter;
using Interface.CCC.Baseline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientVaccinationManager
    {
        private IPatientVaccinationManager _mgr = (IPatientVaccinationManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientVaccination, BusinessProcess.CCC");
        private int _retval;

        public int addPatientVaccination(PatientVaccination patientVaccination)
        {
            return _retval = _mgr.addPatientVaccination(patientVaccination);
        }
    }
}
