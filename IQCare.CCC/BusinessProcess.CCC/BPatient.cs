using DataAccess.Base;
using DataAccess.CCC.Interface;
using Entities.Common;

namespace BusinessProcess.CCC
{
    public class BPatient:ProcessBase
   {
       private UnitOfWork _unitOfWork = new UnitOfWork();
      
       
       public void AddPerson(int id )
       {
           Person personRepository = this.PersonRepository.GetById(id);
       }
        

   }
}
