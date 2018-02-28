using IQCare.HTS.Core.Interfaces;
using IQCare.HTS.Core.Model;
using IQCare.SharedKernel.Infrastructure.Repository;
using System;

namespace IQCare.HTS.Infrastructure.Repository
{
    public class ModuleRepository:BaseRepository<Module, Guid>, IModuleRepository
    {
        public ModuleRepository(HtsDbContext context) : base(context)
        {
        }
    }
}