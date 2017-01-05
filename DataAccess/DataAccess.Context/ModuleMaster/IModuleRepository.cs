using Entities.Administration;
using System.Collections.Generic;

namespace DataAccess.Context.ModuleMaster
{
    public interface IModuleRepository : IRepository<Module>
    {
        Module GetByCode(string code);

        List<ServiceArea> GetModuleServiceArea(int moduleId);


    }
   
}
