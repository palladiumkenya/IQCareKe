using DataAccess.Base;
using DataAccess.Records;
using DataAccess.Records.Context;
using DataAccess.Records.Interface;
using Entities.Records.Enrollment;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.Records
{
   public class BPersonOccupationManager : ProcessBase, IPersonOccupationManager
    { 
        private int result;

        public int AddPersonOccupation(PersonOccupation pated)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                unitOfWork.PersonOccupationRepository.Add(pated);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }

        public int DeletePersonOccupation(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                var personoccupation = unitOfWork.PersonOccupationRepository.GetById(id);
                unitOfWork.PersonOccupationRepository.Remove(personoccupation);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }
        public List<PersonOccupation> GetAllPersonOccupation(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                List<PersonOccupation> mylist;
                mylist = unitOfWork.PersonOccupationRepository.FindBy(x => x.PersonId == personId & !x.DeleteFlag).OrderBy(x => x.Id).ToList();
               
                return mylist;
            }
        }

        public PersonOccupation GetCurrentOccupation(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                PersonOccupation pmo;
                pmo = unitOfWork.PersonOccupationRepository.FindBy(x => x.PersonId == personId & !x.DeleteFlag).OrderByDescending(o => o.CreateDate).FirstOrDefault();
                unitOfWork.Dispose();
                return pmo;
            }
        } 
        public int UpdatePersonOccupation(PersonOccupation pe)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                unitOfWork.PersonOccupationRepository.Update(pe);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }
    }
}

