using Entities.Administration;
using System;
using System.Collections.Generic;

namespace DataAccess.Context.ModuleMaster
{
    public class ModuleRepository : BaseRepository<Module>, IModuleRepository
    {
        private readonly BaseContext _moduleContext;
        public ModuleRepository() : this(new ModuleContext())
        {
            _moduleContext = new ModuleContext();
        }
        public ModuleRepository(BaseContext context) : base(context)
        {
            _moduleContext = context;
        }
        public Module GetByCode(string code)
        {
            throw new NotImplementedException();
        }

        public List<ServiceArea> GetModuleServiceArea(int moduleId)
        {
            throw new NotImplementedException();
        }
    }
}
