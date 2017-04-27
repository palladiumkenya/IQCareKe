using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.Common;
using Interface.CCC;
using System.Text;

namespace BusinessProcess.CCC
{
    public class BPersonContactManager :ProcessBase ,IPersonContactManager
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int _result;

        public int AddPersonContact(PersonContact personContact)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                SqlParameter personIdParameter = new SqlParameter("personIdParameter", SqlDbType.Int);
                personIdParameter.Value = personContact.PersonId;

                SqlParameter physicalAdressParameter = new SqlParameter("physicalAddressParameter", SqlDbType.VarChar);
                physicalAdressParameter.Value =(personContact.PhysicalAddress);

                SqlParameter mobileNumberParameter = new SqlParameter("mobileNumberParameter", SqlDbType.VarChar);
                mobileNumberParameter.Value = (personContact.MobileNumber);

                SqlParameter alternativeNumberParameter = new SqlParameter("alternativeNumberParameter",
                    SqlDbType.VarChar);
                alternativeNumberParameter.Value =(personContact.AlternativeNumber);

                SqlParameter emailAddressParameter = new SqlParameter("emailAddressParameter", SqlDbType.VarChar);
                emailAddressParameter.Value = (personContact.EmailAddress);

                SqlParameter userId = new SqlParameter("UserId", SqlDbType.Int);
                userId.Value = personContact.CreatedBy;

                _unitOfWork.PersonContactRepository.ExecuteProcedure(
                    "exec PersonContact_Insert @personIdParameter,@physicalAddressParameter,@mobileNumberParameter,@alternativeNumberParameter,@emailAddressParameter,@UserId",
                    personIdParameter, physicalAdressParameter, mobileNumberParameter, alternativeNumberParameter,
                    emailAddressParameter, userId);
               _result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public int DeletePersonContact(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                PersonContact personContact = _unitOfWork.PersonContactRepository.GetById(id);
                _unitOfWork.PersonContactRepository.Remove(personContact);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PersonContact> GetAllPersonContact(int personId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                var contacts = _unitOfWork.PersonContactRepository.GetAllPersonContact(personId);
                _unitOfWork.Dispose();
                return contacts;
            }
        }

        public List<PersonContact> GetCurrentPersonContacts(int personId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                var myList = _unitOfWork.PersonContactRepository.GetCurrentPersonContact(personId);
                _unitOfWork.Dispose();
                return myList;
            }
        }

        public int UpdatePersonContact(PersonContact p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
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

                SqlParameter Id = new SqlParameter("Id", SqlDbType.Int);
                Id.Value = p.Id;

                _unitOfWork.PersonContactRepository.ExecuteProcedure(
                    "exec PersonContact_Update @personIdParameter,@physicalAddressParameter,@mobileNumberParameter,@alternativeNumberParameter,@emailAddressParameter, @Id",
                    personIdParameter, physicalAdressParameter, mobileNumberParameter, alternativeNumberParameter,
                    emailAddressParameter, Id);
                _result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }
    }
}
