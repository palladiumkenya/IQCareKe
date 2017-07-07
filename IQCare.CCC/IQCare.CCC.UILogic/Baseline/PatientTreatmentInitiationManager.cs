using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientTreatmentInitiationManager
    {
        private readonly IPatientTreatmentInitiationManager _patientTreatmentInitiation = (IPatientTreatmentInitiationManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientTreatmentInitiationManager, BusinessProcess.CCC");
        private int _id = 0;
        private int _result =0;

        public int ManagePatientTreatmentInititation(int id, int patientId, int patientMasterVisitid, DateTime dateStartedOnFirstLine, string cohort, int regimen, bool ldl, decimal baselineViralload, DateTime baselineViralLoadDate, int userId)
        {
            _id = _patientTreatmentInitiation.CheckIfPatientTreatmentExists(patientId);

            var patientTreatmentInitiationInsert = new PatientTreatmentInitiation
            {
                Id = 0,
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitid,
                DateStartedOnFirstline = dateStartedOnFirstLine,
                Cohort = cohort,
                Regimen = regimen,
                ldl = ldl,
                BaselineViralload = baselineViralload,
                BaselineViralloadDate = baselineViralLoadDate,
                CreatedBy = userId
            };

            _result = (_id > 0)
                ? _patientTreatmentInitiation.UpdatePatientTreatmentInitiation(patientTreatmentInitiationInsert): _patientTreatmentInitiation.AddPatientTreatmentInitiation(patientTreatmentInitiationInsert);
            return _result;
        }

        public int UpdatePatientTreatmentInititation(int id,int patientId, int patientMasterVisitid, DateTime dateStartedOnFirstLine, string cohort, int regimen, bool ldl, decimal baselineViralload, DateTime baselineViralLoadDate)
        {
            var patientTreatmentInitiationUpdate = new PatientTreatmentInitiation
            {
                Id = id,
                DateStartedOnFirstline = dateStartedOnFirstLine,
                Cohort = cohort,
                Regimen = regimen,
                ldl = ldl,
                BaselineViralload = baselineViralload,
                BaselineViralloadDate = baselineViralLoadDate
            };
            return _patientTreatmentInitiation.UpdatePatientTreatmentInitiation(patientTreatmentInitiationUpdate);
        }

        public int DeletePatientTreatmentInitiation(int id)
        {
            return _patientTreatmentInitiation.DeletePatientTreatmentInitiation(id);
        }

        public List<PatientTreatmentInitiation> GetPatientTreatmentInitiationList(int patientId)
        {
            return _patientTreatmentInitiation.GetPatientTreatmentInitiation(patientId);
        }

        public int CheckIfTreatmentInitiationExists(int patientId)
        {
           return  _patientTreatmentInitiation.CheckIfPatientTreatmentExists(patientId);
        }
    }
}
