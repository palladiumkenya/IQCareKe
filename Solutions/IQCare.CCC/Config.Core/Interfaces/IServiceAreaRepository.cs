using Common.Core.Interfaces;
using Config.Core.Model;

namespace Config.Core.Interfaces
{
    public interface IServiceAreaRepository:IRepository<ServiceArea>
    {
        ServiceArea GetByCode(string code);
    }
}