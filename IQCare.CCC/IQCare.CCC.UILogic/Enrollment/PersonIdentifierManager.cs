using Application.Presentation;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using System;
using System.Collections.Generic;

namespace IQCare.CCC.UILogic.Enrollment
{
    public class PersonIdentifierManager
    {
        IPersonIdentifierManager _mgr = (IPersonIdentifierManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Enrollment.BPersonIdentifier, BusinessProcess.CCC");

        public int AddPersonIdentifier(int personId, int identifierId, string identifierValue, int userId, string assigning_facility)
        {
            try
            {
                PersonIdentifier personIdentifier = new PersonIdentifier()
                {
                    PersonId = personId,
                    IdentifierId = identifierId,
                    IdentifierValue = identifierValue,
                    DeleteFlag = false,
                    CreateDate = DateTime.Now,
                    CreatedBy = userId,
                    AssigningFacility= assigning_facility,
                    Active=true
                };

                return _mgr.AddPersonIdentifier(personIdentifier);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PersonIdentifier> GetPersonIdentifiers(int personId, int identifierId)
        {
            try
            {
                return _mgr.GetPersonIdenfiers(personId, identifierId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PersonIdentifier> CheckIfPersonIdentifierExists(string identifierValue, int identifierTypeId)
        {
            try
            {
               // return _mgr .CheckIfIdentifierNumberIsUsed(identifierValue, identifierTypeId);
                var identifierExist = _mgr.CheckIfPersonIdentifierExists(identifierValue, identifierTypeId);
                return identifierExist;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        
    }
}
