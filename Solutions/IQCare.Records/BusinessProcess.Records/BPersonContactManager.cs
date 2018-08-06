using DataAccess.Base;
using DataAccess.Context;
using DataAccess.Records;
using Entities.Common;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.Records
{
   public class BPersonContactManager :ProcessBase ,IPersonContactManager
    {
        private int _result;

        public int AddPersonContact(PersonContact personContact)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                SqlParameter personIdParameter = new SqlParameter("personIdParameter", SqlDbType.Int);
                personIdParameter.Value = personContact.PersonId;

                SqlParameter physicalAdressParameter = new SqlParameter("physicalAddressParameter", SqlDbType.VarBinary);
                physicalAdressParameter.Value = Encoding.ASCII.GetBytes(personContact.PhysicalAddress);

                SqlParameter mobileNumberParameter = new SqlParameter("mobileNumberParameter", SqlDbType.VarBinary);
                mobileNumberParameter.Value = Encoding.ASCII.GetBytes(personContact.MobileNumber);

                SqlParameter alternativeNumberParameter = new SqlParameter("alternativeNumberParameter",
                    SqlDbType.VarBinary);
                alternativeNumberParameter.Value = Encoding.ASCII.GetBytes(personContact.AlternativeNumber);

                SqlParameter emailAddressParameter = new SqlParameter("emailAddressParameter", SqlDbType.VarBinary);
                emailAddressParameter.Value = Encoding.ASCII.GetBytes(personContact.EmailAddress);

                SqlParameter userId = new SqlParameter("UserId", SqlDbType.Int);
                userId.Value = personContact.CreatedBy;

                unitOfWork.PersonContactRepository.ExecuteProcedure(
                    "exec PersonContact_Insert @personIdParameter,@physicalAddressParameter,@mobileNumberParameter,@alternativeNumberParameter,@emailAddressParameter,@UserId",
                    personIdParameter, physicalAdressParameter, mobileNumberParameter, alternativeNumberParameter,
                    emailAddressParameter, userId);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public int DeletePersonContact(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                PersonContact personContact = unitOfWork.PersonContactRepository.GetById(id);
                unitOfWork.PersonContactRepository.Remove(personContact);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PersonContact> GetAllPersonContact(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                var contacts = unitOfWork.PersonContactRepository.GetAllPersonContact(personId);
                unitOfWork.Dispose();
                return contacts;
            }
        }

        public List<PersonContact> GetCurrentPersonContacts(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                var myList = unitOfWork.PersonContactRepository.GetCurrentPersonContact(personId);
                unitOfWork.Dispose();
                return myList;
            }
        }

        public int UpdatePersonContact(PersonContact p)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PersonContext()))
            {
                SqlParameter personIdParameter = new SqlParameter("personIdParameter", SqlDbType.Int);
                personIdParameter.Value = p.PersonId;

                SqlParameter physicalAdressParameter = new SqlParameter("physicalAddressParameter", SqlDbType.VarBinary);
                physicalAdressParameter.Value = Encoding.ASCII.GetBytes(p.PhysicalAddress);

                SqlParameter mobileNumberParameter = new SqlParameter("mobileNumberParameter", SqlDbType.VarBinary);
                mobileNumberParameter.Value = Encoding.ASCII.GetBytes(p.MobileNumber);

                SqlParameter alternativeNumberParameter = new SqlParameter("alternativeNumberParameter",
                    SqlDbType.VarBinary);
                alternativeNumberParameter.Value = Encoding.ASCII.GetBytes(p.AlternativeNumber);

                SqlParameter emailAddressParameter = new SqlParameter("emailAddressParameter", SqlDbType.VarBinary);
                emailAddressParameter.Value = Encoding.ASCII.GetBytes(p.EmailAddress);

                SqlParameter id = new SqlParameter("Id", SqlDbType.Int);
                id.Value = p.Id;

                unitOfWork.PersonContactRepository.ExecuteProcedure(
                    "exec PersonContact_Update @personIdParameter,@physicalAddressParameter,@mobileNumberParameter,@alternativeNumberParameter,@emailAddressParameter, @Id",
                    personIdParameter, physicalAdressParameter, mobileNumberParameter, alternativeNumberParameter,
                    emailAddressParameter, id);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }
    }
}
