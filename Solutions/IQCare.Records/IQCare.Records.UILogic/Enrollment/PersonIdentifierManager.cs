using Application.Presentation;
using Entities.Records.Enrollment;
using Interface.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Records.UILogic.Enrollment
{
    public class PersonIdentifierManager
    {
        IPersonIdentifierManager _mgr = (IPersonIdentifierManager)ObjectFactory.CreateInstance("BusinessProcess.Records.Enrollment.BPersonIdentifier, BusinessProcess.Records");
        private int _result;
        private string _msg;
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

        public int UpdatePersondentifier(int personId, int IdentifierId, string identifierValue, int userId)
        {
            try
            {
                PersonIdentifier personIdentifier = new PersonIdentifier()
                {
                    PersonId = personId,
                    IdentifierId = IdentifierId,
                    IdentifierValue = identifierValue,
                    CreatedBy = userId
                };
                return _mgr.UpdatePersondentifier(personIdentifier);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public string DeletePersonIdentifier(int id)
        {
            try
            {
                _result = _mgr.DeletePersonIdentifier(id);
                if (_result > 0)
                {
                    _msg = "PersonIdenfier deleted successfully";
                }
            }
            catch (Exception e)
            {
                _msg = e.Message + ' ' + e.InnerException;
            }
            return _msg;
        }

        public PersonIdentifier GetCurrentPersonIdentifier(int personId, int identifierId)
        {
            PersonIdentifier mylist;
            try
            {
                mylist = _mgr.GetCurrentPersonIdentifier(personId, identifierId);
                return mylist;
            }
            catch (Exception e)
            {
                _msg = e.Message + ' ' + e.InnerException;
                throw;
            }

        }
    }
    }
