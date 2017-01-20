using System.Collections.Generic;
using DataAccess.Base;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.Common;
using Interface.CCC;
using System.Linq;
using DataAccess.CCC.Repository.person;

namespace BusinessProcess.CCC
{
    public class BPersonManager:ProcessBase, IPersonManager
    {
        private  UnitOfWork _unitOfWork=new UnitOfWork(new PersonContext());

       public int AddPerson(Person person)
       {
            _unitOfWork.PersonRepository.Add(person);
           // p.Add(person);

           return 1;//person.Id;
       }

        public Person GetPerson(int id)
        {
           var personInfo=  _unitOfWork.PersonRepository.GetById(id);
           return personInfo;
        }

        public List<Person> GetPersonAll()
        {
           var mylist= _unitOfWork.PersonRepository.GetAll();
            return mylist.ToList();
        }


        public void UpdatePerson(Person p)
        {
            _unitOfWork.PersonRepository.Update(p);
            _unitOfWork.Complete();
        }

        public void DeletePerson(int id)
        {
            Person personInfo = _unitOfWork.PersonRepository.GetById(id);
            _unitOfWork.PersonRepository.Remove(personInfo);
            _unitOfWork.Complete();
        }
    }
}
