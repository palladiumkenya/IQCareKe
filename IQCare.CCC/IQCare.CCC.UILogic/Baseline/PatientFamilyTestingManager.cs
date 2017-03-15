using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common;
using Application.Presentation;
using Entities.CCC.Baseline;
using Entities.CCC.Lookup;
using Entities.Common;
using Interface.CCC;
using Interface.CCC.Baseline;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientFamilyTestingManager
    {
        private readonly IPatientHivTestingManager _hivTestingManager = (IPatientHivTestingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientHivTestingManager, BusinessProcess.CCC");
        private readonly IPersonManager _personManager = (IPersonManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonManager, BusinessProcess.CCC");
        private readonly IPersonRelationshipManager _personRelationshipManager = (IPersonRelationshipManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonRelationshipManager, BusinessProcess.CCC");
        PersonLookUpManager personLookUp = new PersonLookUpManager();

        public int AddPatientFamilyTestings(PatientFamilyTesting p)
        {
            Person person = new Person()
            {
                FirstName = p.FirstName,
                MidName = p.MiddleName,
                LastName = p.LastName,
                Sex = p.Sex,
                //DateOfBirth = p.DateOfBirth,
            };
            int personId = _personManager.AddPerson(person);

            PersonRelationship relationship = new PersonRelationship()
            {
                PersonId = personId,
                RelatedTo = p.PatientId,
                RelationshipTypeId = p.RelationshipId
            };
            _personRelationshipManager.AddPersonRelationship(relationship);

            DateTime ? baselineDate = p.BaselineHivStatusDate;
            if (baselineDate == DateTime.MinValue)
                baselineDate = null;
            DateTime? testingDate = p.HivTestingResultsDate;
            if (testingDate == DateTime.MinValue)
                testingDate = null;

            PatientHivTesting familyTesting = new PatientHivTesting()
            {
                PersonId = personId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                BaselineResult = p.BaseLineHivStatusId,
                BaselineDate = baselineDate,
                TestingResult = p.HivTestingResultsId,
                TestingDate = testingDate,
                ReferredToCare = p.CccReferal,
                CccNumber = p.CccReferaalNumber
            };
            int hivTestingId = _hivTestingManager.AddPatientHivTesting(familyTesting);
            return hivTestingId;
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
                //DateOfBirth = p.DateOfBirth,
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
                ReferredToCare = p.CccReferal,
                CccNumber = p.CccReferaalNumber
            };
            int hivTestingId = _hivTestingManager.UpdatePatientHivTesting(familyTesting);
            return hivTestingId;
        }

        public List<PatientFamilyTesting> GetPatienFamilyList(int patientId)
        {
            List<PatientFamilyTesting> patientFamilyTestings = new List<PatientFamilyTesting>();
            PatientFamilyTesting familyTesting = null;
            List<PersonRelationship> personRelationships = _personRelationshipManager.GetAllPersonRelationship(patientId);
            Utility utility = new Utility();
            foreach (var relationship in personRelationships)
            {
                var hivTesting = _hivTestingManager.GetAll().FirstOrDefault(n => n.PersonId == relationship.PersonId);
                PersonLookUp person = personLookUp.GetPersonById(relationship.PersonId);
                if (person != null)
                    if (hivTesting != null)
                        familyTesting = new PatientFamilyTesting()
                        {
                            FirstName = person.FirstName,
                            MiddleName = person.MiddleName,
                            LastName = person.LastName,
                            Sex = person.Sex,
                            //DateOfBirth = person.DateOfBirth,
                            PersonId = relationship.PersonId,
                            RelationshipId = relationship.RelationshipTypeId,
                            BaseLineHivStatusId = hivTesting.BaselineResult,
                            BaselineHivStatusDate = hivTesting.BaselineDate,
                            HivTestingResultsId = hivTesting.TestingResult,
                            HivTestingResultsDate = hivTesting.TestingDate,
                            CccReferal = hivTesting.ReferredToCare,
                            CccReferaalNumber = hivTesting.CccNumber
                        };
                patientFamilyTestings.Add(familyTesting);
            }
            return patientFamilyTestings;
        }

        public int GetPatienFamilyCount(int patientId)
        {
            List<PatientFamilyTesting> patientFamilyTestings = new List<PatientFamilyTesting>();
            int personRelationshipsCount = _personRelationshipManager.GetAllPersonRelationship(patientId).Count;
            
            return personRelationshipsCount;
        }
    }
}