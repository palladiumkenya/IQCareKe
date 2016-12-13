using System;
using Config.Core.Interfaces;
using PatientManagement.Core.Interfaces;


namespace Unitofwork.Core.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        IPatientRepository PatientRepository { get; }
        IServiceAreaRepository ServiceAreaRepository { get; }
    }
}
