using System;
using IQCare.HTS.Core.Model;
using IQCare.SharedKernel.Interfaces;

namespace IQCare.HTS.Core.Interfaces
{
    public interface IModuleRepository:IRepository<Module, Guid>
    {
        
    }
}