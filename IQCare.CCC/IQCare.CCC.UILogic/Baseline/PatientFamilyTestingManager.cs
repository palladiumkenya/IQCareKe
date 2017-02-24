using Application.Presentation;
using Entities.CCC.Encounter;
using Interface.CCC.Baseline;
using System.Collections.Generic;
using Entities.CCC.Baseline;
using Entities.Common;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientFamilyTestingManager
    {
        private IPatientHivTesting _hivTesting = (IPatientHivTesting)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientFamilyTesting, BusinessProcess.CCC");

        public int AddPatientFamilyTestings(PatientFamilyTesting p)
        {
            Person person = new Person()
            {
                FirstName = p.FirstName,
                MidName = p.MiddleName,
                LastName = p.LastName,
                Sex = p.Sex,
                DateOfBirth = p.DateOfBirth,
            };

            PatientHivTesting familyTesting = new PatientHivTesting()
            {
                //PersonId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                //RelationshipId = p.RelationshipId,
                BaselineResult = p.BaseLineHivStatusId,
                BaselineDate = p.BaselineHivStatusDate,
                TestingResult = p.HivTestingResultsId,
                TestingDate = p.HivTestingResultsDate,
                ReferredToCare = p.CccReferal
            };
            return _hivTesting.AddPatientHivTesting(familyTesting);
        }

        public PatientHivTesting GetPatientFamilyTestings(int id)
        {
            var familyTesting = _hivTesting.GetPatientHivTesting(id);
            return familyTesting;
        }

        public void DeletePatientFamilyTestings(int id)
        {
            _hivTesting.DeletePatientHivTesting(id);
        }

        public int UpdatePatientFamilyTestings(PatientFamilyTesting p)
        {
            Person person = new Person()
            {
                FirstName = p.FirstName,
                MidName = p.MiddleName,
                LastName = p.LastName,
                Sex = p.Sex,
                DateOfBirth = p.DateOfBirth,
            };

            PatientHivTesting familyTesting = new PatientHivTesting()
            {
                //PersonId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                //RelationshipId = p.RelationshipId,
                BaselineResult = p.BaseLineHivStatusId,
                BaselineDate = p.BaselineHivStatusDate,
                TestingResult = p.HivTestingResultsId,
                TestingDate = p.HivTestingResultsDate,
                ReferredToCare = p.CccReferal
            };
            return _hivTesting.UpdatePatientHivTesting(familyTesting);
        }
    }
}