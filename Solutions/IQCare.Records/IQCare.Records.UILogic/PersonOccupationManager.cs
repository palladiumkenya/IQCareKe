using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.Records;
using Entities.Records.Enrollment;
using Interface.Records;
namespace IQCare.Records.UILogic
{
    public class PersonOccupationManager
    {
        private IPersonOccupationManager _mgr = (IPersonOccupationManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BBPersonOccupationManager,BusinessProces.Records");
        private int _result;
        private string _msg;


        public int AddPersonOccupation(int userId,int occupation,int personId)
        {
            PersonOccupation pm = new PersonOccupation()
            {
                PersonId = personId,
                CreatedBy = userId,
                Occupation = occupation
            };

           _result= _mgr.AddPersonOccupation(pm);
            return _result;
        }

        public  string  DeletePersonPersonOccupation(int id)
        {
            try
            {
                _result = _mgr.DeletePersonOccupation(id);
                if (_result > 0)
                {
                    _msg = "PersonOccupation deleted successfully";
                }
            }
            catch (Exception e)
            {
                _msg = e.Message + ' ' + e.InnerException;
            }
            return _msg;

        }

        public List<PersonOccupation> GetAllPersonOccupation(int personId)
        {
            List<PersonOccupation> mylist;
            try
            {
                mylist = _mgr.GetAllPersonOccupation(personId);
                return mylist;
            }
            catch (Exception e)
            {
                _msg = e.Message + ' ' + e.InnerException;
                throw;
            }

        }
        public PersonOccupation GetCurrentOccupationion(int personId)
        {
            PersonOccupation mylist;
            try
            {
                mylist = _mgr.GetCurrentOccupation(personId);
                return mylist;
            }
            catch (Exception e)
            {
                _msg = e.Message + ' ' + e.InnerException;
                throw;
            }

        }

        public int UpdatePersonOccupation(int personId, int userId, int occupation)
        {
            PersonOccupation pm = new PersonOccupation()
            {
                PersonId = personId,
                Occupation=occupation,
                CreatedBy = userId
            };
            _result = _mgr.UpdatePersonOccupation(pm);
            return _result;
        }

    }
}
