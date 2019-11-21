using Entities.Administration;
using System.Collections.Generic;

namespace DataAccess.Context.ModuleMaster
{
    public interface IModuleRepository : IRepository<Module>
    {
        Module GetByName(string name);

        List<ServiceArea> GetModuleServiceArea(int moduleId);


    }
   
}
