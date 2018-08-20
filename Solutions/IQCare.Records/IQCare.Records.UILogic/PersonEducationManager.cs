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
    public class PersonEducationManager
    {
        private IPersonEducationManager _mgr = (IPersonEducationManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BPersonEducationManager,BusinessProcess.Records");
        private int _result;
        private string _msg;


        public int AddPersonEducationLevel(int PersonId, int UserId, int EducationalLevel)
        {
            PersonEducation pm = new PersonEducation()
            {
                PersonId = PersonId,
                EducationLevel = EducationalLevel,
                CreatedBy = UserId
            };
            _result = _mgr.AddPersonEducationLevel(pm);
            return _result;

        }
        public string DeletePersonEducationalLevel(int id)
        {
            try
            {
                _result = _mgr.DeletePersonEducationLevel(id);
                if (_result > 0)
                {
                    _msg = "PersonEducationalLevel deleted successfully";
                }
            }
            catch (Exception e)
            {
                _msg = e.Message + ' ' + e.InnerException;
            }
            return _msg;
        }

        public List<PersonEducation> GetAllPersonEducationalLevel(int personId)
        {
            List<PersonEducation> mylist;
            try
            {
                mylist = _mgr.GetAllPersonEducationLevel(personId);
                return mylist;
            }
            catch(Exception e)
            {
                _msg=e.Message + ' ' + e.InnerException;
                throw;
            }

        }
        public PersonEducation GetCurrentPersonEducation(int personId)
        {
            PersonEducation mylist;
            try
            {
                mylist = _mgr.GetCurrentPersonEducation(personId);
                return mylist;
            }
            catch (Exception e)
            {
                _msg = e.Message + ' ' + e.InnerException;
                throw;
            }

        }

        public int UpdatePersonEducationalLevel(int PersonId, int UserId, int EducationalLevel)
        {
            PersonEducation pm = new PersonEducation()
            {
                PersonId = PersonId,
                EducationLevel = EducationalLevel,
                CreatedBy = UserId
            };
            _result = _mgr.UpdatePersonEducation(pm);
            return _result;
        }
      
   
    }
}
