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

        private readonly IPatientLinkageManager linkageManager =
            (IPatientLinkageManager) ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientLinkageManager, BusinessProcess.CCC");

        PersonLookUpManager personLookUp = new PersonLookUpManager();

        public int AddPatientFamilyTestings(PatientFamilyTesting p, int userId)
        {
            PersonManager pm = new PersonManager();
            int personId =  pm.AddPersonUiLogic(p.FirstName, p.MiddleName, p.LastName, p.Sex, userId, p.DateOfBirth, p.DobPrecision);

            PersonRelationship relationship = new PersonRelationship()
            {
                PersonId = personId,
                PatientId = p.PatientId,
                BaselineResult = p.BaseLineHivStatusId,
                BaselineDate = p.BaselineHivStatusDate,
                RelationshipTypeId = p.RelationshipId,
                CreatedBy = userId
            };
            _personRelationshipManager.AddPersonRelationship(relationship);

            DateTime? testingDate = p.HivTestingResultsDate;
            if (testingDate == DateTime.MinValue)
                testingDate = null;

            PatientHivTesting familyTesting = new PatientHivTesting()
            {
                PersonId = personId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                TestingResult = p.HivTestingResultsId,
                TestingDate = testingDate,
                ReferredToCare = p.CccReferal,
                CreatedBy = userId
            };
            int hivTestingId = _hivTestingManager.AddPatientHivTesting(familyTesting);

            if (p.CccReferal == true)
            {
                PatientLinkage patientLinkage = new PatientLinkage()
                {
                    PersonId = personId,
                    LinkageDate = (DateTime)p.LinkageDate,
                    CCCNumber = p.CccReferaalNumber,
                    CreatedBy = userId
                };

                linkageManager.AddPatientLinkage(patientLinkage);
            }
            
            return hivTestingId;
        }

        public void AddLinkedPatientFamilyTesting(int personId, int patientId, int patientMasterVisitId, int baselineResult, DateTime baselineDate, int relationshipTypeId, int userId, string cccNumber)
        {
            PersonRelationship relationship = new PersonRelationship();
            relationship.PersonId = personId;
            relationship.PatientId = patientId;
            relationship.BaselineResult = baselineResult;
            relationship.BaselineDate = baselineDate;
            relationship.RelationshipTypeId = relationshipTypeId;
            relationship.CreatedBy = userId;


            _personRelationshipManager.AddPersonRelationship(relationship);

            PatientHivTesting familyTesting = new PatientHivTesting()
            {
                PersonId = personId,
                PatientMasterVisitId = patientMasterVisitId,
                TestingResult = 0,
                TestingDate = null,
                ReferredToCare = true,
                CreatedBy = userId
            };

            int hivTestingId = _hivTestingManager.AddPatientHivTesting(familyTesting);

            PatientLinkage patientLinkage = new PatientLinkage()
            {
                PersonId = personId,
                LinkageDate = DateTime.Now,
                CCCNumber = cccNumber,
                CreatedBy = userId
            };

            linkageManager.AddPatientLinkage(patientLinkage);
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
            //pm.UpdatePerson(p.FirstName, p.MiddleName, p.LastName, p.Sex, userId, p.PersonId, p.DateOfBirth);
            //Person person = new Person()
            //{
            //    FirstName = _utility.Encrypt(p.FirstName),
            //    MidName = _utility.Encrypt(p.MiddleName),
            //    LastName = _utility.Encrypt(p.LastName),
            //    Sex = p.Sex,
            //    //DateOfBirth = p.DateOfBirth,
            //};
           // int personId = _personManager.UpdatePerson(person, p.PersonId);

            //PersonRelationship relationship = new PersonRelationship()
            //{
            //    Id = p.PersonRelationshipId,
            //    PersonId = personId,
            //    PatientId = p.PatientId,
            //    //RelatedTo = p.PatientId,
            //    RelationshipTypeId = p.RelationshipId
            //};
            //_personRelationshipManager.UpdatePersonRelationship(relationship);

            PatientHivTesting familyTesting = new PatientHivTesting()
            {
                PersonId = personId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                TestingResult = p.HivTestingResultsId,
                TestingDate = p.HivTestingResultsDate,
                ReferredToCare = p.CccReferal,
                CreatedBy = userId
            };

            int hivTestingId = _hivTestingManager.AddPatientHivTesting(familyTesting);

            if (p.CccReferal == true)
            {
                PatientLinkage patientLinkage = new PatientLinkage()
                {
                    PersonId = personId,
                    LinkageDate = (DateTime)p.LinkageDate,
                    CCCNumber = p.CccReferaalNumber,
                    CreatedBy = userId
                };

                linkageManager.AddPatientLinkage(patientLinkage);
            }
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
                var hivTesting = _hivTestingManager.GetAll().OrderByDescending(y=>y.Id).FirstOrDefault(n => n.PersonId == relationship.PersonId);
                PersonLookUp person = personLookUp.GetPersonById(relationship.PersonId);
                var linkage = linkageManager.GetPatientLinkage(relationship.PersonId).FirstOrDefault();

                if (person != null)
                {
                    familyTesting = new PatientFamilyTesting()
                    {
                        FirstName = (person.FirstName),
                        MiddleName = (person.MiddleName),
                        LastName = (person.LastName),
                        Sex = person.Sex,
                        DateOfBirth = person.DateOfBirth == null ? DateTime.Now : (DateTime)person.DateOfBirth.Value,
                        DobPrecision = person.DobPrecision == null ? false : Convert.ToBoolean(person.DobPrecision),
                        PersonId = relationship.PersonId,
                        RelationshipId = relationship.RelationshipTypeId,
                        BaseLineHivStatusId = relationship.BaselineResult,
                        BaselineHivStatusDate = relationship.BaselineDate,
                        //HivTestingResultsId = hivTesting.TestingResult,
                        //HivTestingResultsDate = hivTesting.TestingDate,
                        //CccReferal = hivTesting.ReferredToCare,
                        //CccReferaalNumber = linkage.CCCNumber,
                        //LinkageDate = linkage.LinkageDate,
                        PersonRelationshipId = relationship.Id
                        //HivTestingId = hivTesting.Id
                    };

                    if (hivTesting != null)
                    {
                        familyTesting.HivTestingResultsId = hivTesting.TestingResult;
                        familyTesting.HivTestingResultsDate = hivTesting.TestingDate;
                        familyTesting.CccReferal = hivTesting.ReferredToCare;
                        familyTesting.HivTestingId = hivTesting.Id;
                    }

                    if (linkage != null)
                    {
                        familyTesting.CccReferaalNumber = linkage.CCCNumber;
                        familyTesting.LinkageDate = linkage.LinkageDate;
                    }
                    patientFamilyTestings.Add(familyTesting);
                }
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