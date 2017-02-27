using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientTreatmentInitiationManager
    {
        private readonly IPatientTreatmentInitiationManager _patientTreatmentInitiation = (IPatientTreatmentInitiationManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientTreatmeantInitiationManager, BusinessProcess.CCC");

        public int AddPatientTreatmentInititation(int id,int patientId,int patientMasterVisitid,DateTime dateStartedOnFirstLine,string cohort,int regimen,decimal baselineViralload,DateTime baselineViralLoadDate,int userId)
        {
            var patientTreatmentInitiationInsert = new PatientTreatmentInitiation
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitid,
                DateStartedOnFirstline = dateStartedOnFirstLine,
                Cohort = cohort,
                Regimen = regimen,
                BaselineViralload = baselineViralload,
                BaselineViralloadDate = baselineViralLoadDate,
                CreatedBy = userId
            };

            return _patientTreatmentInitiation.AddPatientTreatmentInitiation(patientTreatmentInitiationInsert);
        }

        public int UpdatePatientTreatmentInititation(int id,int patientId, int patientMasterVisitid, DateTime dateStartedOnFirstLine, string cohort, int regimen, decimal baselineViralload, DateTime baselineViralLoadDate)
        {
            var patientTreatmentInitiationUpdate = new PatientTreatmentInitiation
            {
                Id = id,
                DateStartedOnFirstline = dateStartedOnFirstLine,
                Cohort = cohort,
                Regimen = regimen,
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
    }
}
