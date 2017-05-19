using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Encounter;
using Interface.CCC.Baseline;

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

        public int UpdatePatientVaccination(PatientVaccination patientVaccination)
        {
            return _retval = _mgr.updatePatientVaccination(patientVaccination);
        }

        public List<PatientVaccination> GetPatientVaccinations(int patientId)
        {
            try
            {
                return _mgr.GetPatientVaccinations(patientId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<PatientVaccination> VaccineExists(int patientId, int vaccine, string vaccineStage)
        {
            try
            {
                return _mgr.VaccineExists(patientId, vaccine, vaccineStage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
