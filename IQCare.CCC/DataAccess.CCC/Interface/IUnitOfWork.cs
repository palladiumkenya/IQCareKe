using DataAccess.Context.ModuleMaster;
using System;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.CCC.Interface.person;

namespace DataAccess.CCC.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        
        IModuleRepository ModuleRepository { get; }

        //LookupContext
        ILookupRepository LookupRepository { get; }
        ILookupMasterRepository LookupMasterRepository { get; }

        // person and patient
        IPersonRepository PersonRepository { get; }
        IPersonLocationRepository PersonLocationRepository { get; }
        IPersonContactRepository PersonContactRepository { get; }
        IPersonRelationshipRepository PersonRelationshipRepository { get; }
        IPatientOvcStatusRepository PatientOvcStatusRepository { get; }
        IPatientMaritalStatusRepository PatientMaritalStatusRepository { get; }
    }
}
