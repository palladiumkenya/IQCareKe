using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.CCC.Enrollment;
using Entities.PatientCore;
using Interface.CCC.Enrollment;

namespace IQCare.CCC.UILogic.Enrollment
{
    public class PersonIdentifierManager
    {
        IPersonIdentifierManager _mgr = (IPersonIdentifierManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Enrollment.BPersonIdentifier, BusinessProcess.CCC");

        public int AddPersonIdentifier(int personId, int identifierId, string identifierValue, int userId)
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
                    CreatedBy = userId
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
