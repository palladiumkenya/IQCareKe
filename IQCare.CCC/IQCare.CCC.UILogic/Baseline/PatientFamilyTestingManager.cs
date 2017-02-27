using Application.Presentation;
using Entities.CCC.Encounter;
using Interface.CCC.Baseline;
using System.Collections.Generic;
using Entities.CCC.Baseline;
using Entities.Common;
using Interface.CCC;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientFamilyTestingManager
    {
        private readonly IPatientHivTestingManager _hivTestingManager = (IPatientHivTestingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientHivTestingManager, BusinessProcess.CCC.Baseline");
        private readonly IPersonManager _personManager = (IPersonManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonManager, BusinessProcess.CCC");
        private readonly IPersonRelationshipManager _personRelationshipManager = (IPersonRelationshipManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonRelationshipManager, BusinessProcess.CCC");

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
            int personId = _personManager.AddPerson(person);

            PersonRelationship relationship = new PersonRelationship()
            {
                PersonId = personId,
                RelatedTo = p.PatientId,
                RelationshipTypeId = p.RelationshipId
            };
            _personRelationshipManager.AddPersonRelationship(relationship);

            PatientHivTesting familyTesting = new PatientHivTesting()
            {
                PersonId = personId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                BaselineResult = p.BaseLineHivStatusId,
                BaselineDate = p.BaselineHivStatusDate,
                TestingResult = p.HivTestingResultsId,
                TestingDate = p.HivTestingResultsDate,
                ReferredToCare = p.CccReferal
            };
            return _hivTestingManager.AddPatientHivTesting(familyTesting);
        }

        public PatientHivTesting GetPatientFamilyTestings(int id)
        {
            var familyTesting = _hivTestingManager.GetPatientHivTesting(id);
            return familyTesting;
        }

        public void DeletePatientFamilyTestings(int id)
        {
            _hivTestingManager.DeletePatientHivTesting(id);
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
            int personId = _personManager.UpdatePerson(person, p.PersonId);

            PersonRelationship relationship = new PersonRelationship()
            {
                PersonId = personId,
                RelatedTo = p.PatientId,
                RelationshipTypeId = p.RelationshipId
            };
            _personRelationshipManager.UpdatePersonRelationship(relationship);

            PatientHivTesting familyTesting = new PatientHivTesting()
            {
                PersonId = personId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                BaselineResult = p.BaseLineHivStatusId,
                BaselineDate = p.BaselineHivStatusDate,
                TestingResult = p.HivTestingResultsId,
                TestingDate = p.HivTestingResultsDate,
                ReferredToCare = p.CccReferal
            };
            return _hivTestingManager.UpdatePatientHivTesting(familyTesting);
        }
    }
}