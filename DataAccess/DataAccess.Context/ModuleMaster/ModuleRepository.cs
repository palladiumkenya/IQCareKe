using Entities.Administration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Context.ModuleMaster
{
    public class ModuleRepository : BaseRepository<Module>, IModuleRepository
    {
        private readonly ModuleContext _moduleContext;
        public ModuleRepository() : this(new ModuleContext())
        {
            _moduleContext = new ModuleContext();
        }
        public ModuleRepository(ModuleContext context) : base(context)
        {
            _moduleContext = context;
        }

        protected ModuleRepository(BaseContext context) : base(context)
        {
        }

        public Module GetByName(string name)
        {
           var mod = _moduleContext.Modules.Where(md => md.Name == name).FirstOrDefault();

            return mod;
        }

        public List<ServiceArea> GetModuleServiceArea(int moduleId)
        {
            throw new NotImplementedException();
        }
    }
}
