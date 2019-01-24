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
            int vaccineId = 0;
            List<PatientVaccination> patientVaccinationList = _mgr.VaccineExists(patientVaccination.PatientId, patientVaccination.Vaccine, patientVaccination.VaccineStage);
            foreach (var value in patientVaccinationList)
            {
                vaccineId = value.Id;
            }
            if (vaccineId > 0)
            {
                return _retval = 0;
            }
            else
            {
                return _retval = _mgr.addPatientVaccination(patientVaccination);
            }
        }

        public int UpdatePatientVaccination(PatientVaccination patientVaccination)
        {
            return _retval = _mgr.updatePatientVaccination(patientVaccination);
        }

        public void deletePatientVaccination(int immunizationId)
        {
            try
            {
                int patientId = 0;
                int patientMasterVisitId = 0;
                int vaccine = 0;
                string vaccineStage = "";
                DateTime? vaccineDate = new DateTime();
                List<PatientVaccination> patientVaccinationList = _mgr.GetPatientVaccinationsById(immunizationId);
                foreach(var value in patientVaccinationList)
                {
                    patientId = value.PatientId;
                    patientMasterVisitId = value.PatientMasterVisitId;
                    vaccine = value.Vaccine;
                    vaccineStage = value.VaccineStage;
                    vaccineDate = value.VaccineDate;
                }
                PatientVaccination patientVaccination = new PatientVaccination()
                {
                    Id = immunizationId,
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    Vaccine = vaccine,
                    VaccineStage = vaccineStage,
                    VaccineDate = vaccineDate,
                    DeleteFlag = true
                };
                _mgr.deletePatientVaccination(patientVaccination);
            }
            catch(Exception e)
            {
                throw;
            }
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
