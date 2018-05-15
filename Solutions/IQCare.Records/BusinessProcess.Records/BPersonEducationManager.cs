using DataAccess.Base;
using DataAccess.Records;
using DataAccess.Records.Context;
using DataAccess.Records.Repository;
using Entities.Records.Enrollment;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.Records
{
    public class BPersonEducationManager : ProcessBase, IPersonEducationManager
    {
        private int result;

        public int AddPersonEducationLevel(PersonEducation   pated)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                unitOfWork.PersonEducationRepository.Add(pated);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }

        public int DeletePersonEducationLevel(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                var personeducation = unitOfWork.PersonEducationRepository.GetById(id);
                unitOfWork.PersonEducationRepository.Remove(personeducation);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }
        public List<PersonEducation> GetAllPersonEducationLevel(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                List<PersonEducation> mylist;
                mylist = unitOfWork.PersonEducationRepository.FindBy(x => x.PersonId == personId & !x.DeleteFlag).OrderBy(x => x.Id).ToList();

                return mylist;
            }
        }
        public PersonEducation GetCurrentPersonEducation(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                PersonEducation patienteducation = unitOfWork.PersonEducationRepository.FindBy(x => x.PersonId == personId & x.DeleteFlag == false).OrderByDescending(x => x.Id).Take(1).FirstOrDefault();
                unitOfWork.Dispose();
                return patienteducation;

            }
        }
        public int UpdatePersonEducation (PersonEducation pe)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                unitOfWork.PersonEducationRepository.Update(pe);
               result= unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }
    }
}
