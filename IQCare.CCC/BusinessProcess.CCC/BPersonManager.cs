using DataAccess.Base;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.Common;
using Interface.CCC;
using System.Linq;

namespace BusinessProcess.CCC
{
    public class BPersonManager:ProcessBase, IPersonManager
    {
        private  UnitOfWork _unitOfWork=new UnitOfWork(new PersonContext());

       public int AddPerson(Person person)
       {
           // person.FirstName = Utils.
           _unitOfWork.PersonRepository.Add(person);
           _unitOfWork.Complete();

           return person.Id;
       }

        public Person GetPerson(int id)
        {
           var personInfo=  _unitOfWork.PersonRepository.GetById(id);
           return personInfo;
        }

        public void DeletePerson(int id)
        {
            Person personInfo = _unitOfWork.PersonRepository.GetById(id);
            _unitOfWork.PersonRepository.Remove(personInfo);

        }
       
   }
}
