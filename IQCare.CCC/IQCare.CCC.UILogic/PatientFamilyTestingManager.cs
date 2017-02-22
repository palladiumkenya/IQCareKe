using Entities.CCC.Encounter;
using System;
using System.Collections.Generic;
using Application.Presentation;
using Interface.CCC;

namespace IQCare.CCC.UILogic
{
    public class PatientFamilyTestingManager
    {
        private IPatientFamilyTesting _familyTesting =  (IPatientFamilyTesting)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientFamilyTesting, BusinessProcess.CCC");
        public int AddPatientFamilyTestings(PatientFamilyTesting p)
        {
            PatientFamilyTesting familyTesting = new PatientFamilyTesting()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                Name = p.Name,
                RelationshipId = p.RelationshipId,
                BaseLineHivStatusId = p.BaseLineHivStatusId,
                BaselineHivStatusDate = p.BaselineHivStatusDate,
                HivTestingResultsId = p.HivTestingResultsId,
                HivTestingResultsDate = p.HivTestingResultsDate,
                CccReferal = p.CccReferal
            };
            return _familyTesting.AddPatientFamilyTestings(familyTesting);
        }

        public PatientFamilyTesting GetPatientFamilyTestings(int id)
        {
            var familyTesting = _familyTesting.GetPatientFamilyTestings(id);
            return familyTesting;
        }

        public void DeletePatientFamilyTestings(int id)
        {
            _familyTesting.DeletePatientFamilyTestings(id);
        }

        public int UpdatePatientFamilyTestings(PatientFamilyTesting p)
        {
            PatientFamilyTesting familyTesting = new PatientFamilyTesting()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                Name = p.Name,
                RelationshipId = p.RelationshipId,
                BaseLineHivStatusId = p.BaseLineHivStatusId,
                BaselineHivStatusDate = p.BaselineHivStatusDate,
                HivTestingResultsId = p.HivTestingResultsId,
                HivTestingResultsDate = p.HivTestingResultsDate,
                CccReferal = p.CccReferal
            };
            return _familyTesting.UpdatePatientFamilyTestings(familyTesting);
        }

        public List<PatientFamilyTesting> GetByPatientId(int patientId)
        {
            var familyTestings = _familyTesting.GetByPatientId(patientId);
            return familyTestings;
        }
    }
}