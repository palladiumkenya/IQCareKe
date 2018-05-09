using Application.Presentation;
using Entities.Common;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Records.UILogic
{
   
        public class PersonContactManager
        {
            private int _result;
            IPersonContactManager _personContact = (IPersonContactManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BPersonContactManager, BusinessProcess.Records");

            public int AddPersonContact(int personId, string physicalAddress, string mobileNumber, string alternativeNumber, string emailAddress, int userId)
            {
                //Utility x = new Utility();

                //if (alternativeNumber != null)
                //{
                //    alternativeNumber = (alternativeNumber);
                //}
                //if (emailAddress != null)
                //{
                //    emailAddress = x.Encrypt(emailAddress);
                //}

                PersonContact personContact = new PersonContact
                {
                    PersonId = personId,
                    PhysicalAddress = physicalAddress,
                    MobileNumber = mobileNumber,
                    AlternativeNumber = alternativeNumber,
                    EmailAddress = emailAddress,
                    CreatedBy = userId
                };
                _result = this._personContact.AddPersonContact(personContact);
                return _result;
            }

            public int DeletePersonContact(int id)
            {
                return _result = _personContact.DeletePersonContact(id);
            }

            public int UpdatePatientContact(PersonContact personContact)
            {
                return _result = this._personContact.UpdatePersonContact(personContact);
            }

            public List<PersonContact> GetPersonContactList(int personId)
            {
                List<PersonContact> myList = new List<PersonContact>();
                try
                {
                    myList = _personContact.GetAllPersonContact(personId);
                }
                catch (Exception)
                {

                    throw;
                }

                return myList;
            }
        }
    }
