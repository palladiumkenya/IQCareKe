using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Application.Common;
using Application.Presentation;
using Entities.Common;
using Interface.CCC;

namespace IQCare.CCC.UILogic
{
    public class PersonContactManager
    {
        private string _jsonMessage;

        public string AddPersonContactUi(int personId, string physicalAddress, string mobileNumber)
        {
            try
            {
                Utility x = new Utility();

                PersonContact personContact = new PersonContact
                {
                    PersonId = personId,
                    PhysicalAddress = x.Encrypt(physicalAddress),
                    MobileNumber = x.Encrypt(mobileNumber)
                };

                IPersonContactManager mgr =
                    (IPersonContactManager)
                    ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonContactManager, BusinessProcess.CCC");
                mgr.AddPersonContact(personContact);
                _jsonMessage = "New Person Contact Added Successfully!";
            }
            catch (Exception exception)
            {
                _jsonMessage = exception.Message.ToString();
            }

            return _jsonMessage;
        }

        public string DeletePersonContact(int id)
        {
            try
            {
                IPersonContactManager mgr =
                    (IPersonContactManager)
                    ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonContactManager, BusinessProcess.CCC");
                mgr.DeletePersonContact(id);
                _jsonMessage = "Person Contact Delete Successfully!";
            }
            catch (Exception exception)
            {

                _jsonMessage = exception.Message.ToString();
            }

            return _jsonMessage;
        }

        public string UpdatePatientContact(PersonContact personContact)
        {
            try
            {
                IPersonContactManager mgr =(IPersonContactManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonContactManager, BusinessProcess.CCC");
                mgr.UpdatePersonContact(personContact);
                _jsonMessage = "Person Contact Updated Successfuly!";
            }
            catch (Exception exception)
            {
                _jsonMessage = exception.Message.ToString();
            }

            return _jsonMessage;
        }

        public List<PersonContact> GetPersonContactList(int personId)
        {
            List<PersonContact> myList=new List<PersonContact>();
            try
            {
                IPersonContactManager mgr = (IPersonContactManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonContactManager, BusinessProcess.CCC");
                myList = mgr.GetAllPersonContact(personId);
            }
            catch (Exception)
            {
                
                throw;
            }

            return myList;
        }
    }
}
