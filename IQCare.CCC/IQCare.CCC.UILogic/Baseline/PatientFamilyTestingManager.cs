using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.Common;
using Application.Presentation;
using Entities.CCC.Baseline;
using Entities.CCC.Lookup;
using Entities.Common;
using Interface.CCC;
using Interface.CCC.Baseline;
using IQCare.Web.UILogic;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientFamilyTestingManager
    {
        private readonly IPatientHivTestingManager _hivTestingManager = (IPatientHivTestingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientHivTestingManager, BusinessProcess.CCC");
        private readonly IPersonManager _personManager = (IPersonManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonManager, BusinessProcess.CCC");
        private readonly IPersonRelationshipManager _personRelationshipManager = (IPersonRelationshipManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonRelationshipManager, BusinessProcess.CCC");
        PersonLookUpManager personLookUp = new PersonLookUpManager();
        Utility _utility = new Utility();

        public int AddPatientFamilyTestings(PatientFamilyTesting p, int userId)
        {
            PersonManager pm = new PersonManager();
            int personId =  pm.AddPersonUiLogic(p.FirstName, p.MiddleName, p.LastName, p.Sex, userId);
            //Person person = new Person()
            //{
            //    FirstName = _utility.Encrypt(p.FirstName),
            //    MidName = _utility.Encrypt(p.MiddleName),
            //    LastName = _utility.Encrypt(p.LastName),
            //    Sex = p.Sex,
            //    //DateOfBirth = p.DateOfBirth,
            //};
            //int personId = _personManager.AddPerson(person);

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
            pm = null;
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

        public int UpdatePatientFamilyTestings(PatientFamilyTesting p, int userId)
        {
            PersonManager pm = new PersonManager();
            int personId = p.PersonId;
            pm.UpdatePerson(p.FirstName, p.MiddleName, p.LastName, p.Sex, userId, p.PersonId);
            //Person person = new Person()
            //{
            //    FirstName = _utility.Encrypt(p.FirstName),
            //    MidName = _utility.Encrypt(p.MiddleName),
            //    LastName = _utility.Encrypt(p.LastName),
            //    Sex = p.Sex,
            //    //DateOfBirth = p.DateOfBirth,
            //};
           // int personId = _personManager.UpdatePerson(person, p.PersonId);

            PersonRelationship relationship = new PersonRelationship()
            {
                Id = p.PersonRelationshipId,
                PersonId = personId,
                RelatedTo = p.PatientId,
                RelationshipTypeId = p.RelationshipId
            };
            _personRelationshipManager.UpdatePersonRelationship(relationship);

            PatientHivTesting familyTesting = new PatientHivTesting()
            {
                Id = p.HivTestingId,
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
            pm = null;
            familyTesting = null;
            return hivTestingId;
        }

        public List<PatientFamilyTesting> GetPatientFamilyList(int patientId)
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
                            FirstName = (person.FirstName),
                            MiddleName = (person.MiddleName),
                            LastName = (person.LastName),
                            Sex = person.Sex,
                            //DateOfBirth = person.DateOfBirth,
                            PersonId = relationship.PersonId,
                            RelationshipId = relationship.RelationshipTypeId,
                            BaseLineHivStatusId = hivTesting.BaselineResult,
                            BaselineHivStatusDate = hivTesting.BaselineDate,
                            HivTestingResultsId = hivTesting.TestingResult,
                            HivTestingResultsDate = hivTesting.TestingDate,
                            CccReferal = hivTesting.ReferredToCare,
                            CccReferaalNumber = hivTesting.CccNumber,
                            PersonRelationshipId = relationship.Id,
                            HivTestingId = hivTesting.Id
                        };
                patientFamilyTestings.Add(familyTesting);
            }
            return patientFamilyTestings;
        }

        public int GetPatientFamilyCount(int patientId)
        {
            List<PatientFamilyTesting> patientFamilyTestings = new List<PatientFamilyTesting>();
            int personRelationshipsCount = _personRelationshipManager.GetAllPersonRelationship(patientId).Count;
            
            return personRelationshipsCount;
        }
    }
}