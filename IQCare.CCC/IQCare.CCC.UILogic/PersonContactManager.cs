using System;
using System.Collections.Generic;
using Application.Common;
using Application.Presentation;
using Entities.Common;
using Interface.CCC;

namespace IQCare.CCC.UILogic
{
    public class PersonContactManager
    {
        private int _result;
        IPersonContactManager personContact =  (IPersonContactManager) ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonContactManager, BusinessProcess.CCC");

        public int AddPersonContact(int personId, string physicalAddress, string mobileNumber,string alternativeNumber,string emailAddress,int userId)
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
            _result = this.personContact.AddPersonContact(personContact);
            return _result;
        }

        public int DeletePersonContact(int id)
        {
            return _result = personContact.DeletePersonContact(id);
        }

        public int UpdatePatientContact(PersonContact personContact)
        {
            return _result = this.personContact.UpdatePersonContact(personContact);
        }

        public List<PersonContact> GetPersonContactList(int personId)
        {
            List<PersonContact> myList=new List<PersonContact>();
            try
            {
                myList = personContact.GetAllPersonContact(personId);
            }
            catch (Exception)
            {
                
                throw;
            }

            return myList;
        }
    }
}
