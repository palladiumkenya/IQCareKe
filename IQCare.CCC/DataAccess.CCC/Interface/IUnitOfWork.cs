using DataAccess.CCC.Repository;
using DataAccess.CCC.Repository.Patient;
using DataAccess.Context.ModuleMaster;
using System;
using DataAccess.CCC.Interface.Lookup;

namespace DataAccess.CCC.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        
        IModuleRepository ModuleRepository { get; }

        //Patient Context Interfaces
        //ICCCPatientRepository CCCPatientRepository { get; }
        //IPatientContactRepository PatientContactRepository { get; }
        //IPatientEnrollmentRepository PatientEnrollmentRepository { get; }
        //IPatientLocationRepository PatientLocationRepository { get; }
        //IPatientMaritalStatusRepository PatientMaritalStatusRepository { get; }
        //IPatientOVCStatusRepository PatientOvcStatusRepository { get; }
        //IPatientPopulationRepository PatientPopulationRepository { get; }
        //IPatientTreatmentSupporterRepository PatientTreatmentSupporterRepository { get; }

        //LookupContext
        ILookupRepository LookupRepository { get; }
        ILookupMasterRepository LookupMasterRepository { get; }
    }
}
