using DataAccess.Context.ModuleMaster;
using System;

namespace DataAccess.CCC.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        
        IModuleRepository ModuleRepository { get; }

        //Patient Context Interfaces
        IPatientRepository PatientRepository { get; }
        IPatientContactRepository PatientContactRepository { get; }
        IPatientEnrollmentRepository PatientEnrollmentRepository { get; }
        IPatientLocationRepository PatientLocationRepository { get; }
        IPatientMaritalStatusRepository PatientMaritalStatusRepository { get; }
        IPatientOVCStatusRepository PatientOvcStatusRepository { get; }
        IPatientPopulationRepository PatientPopulationRepository { get; }
        IPatientTreatmentSupporterRepository PatientTreatmentSupporterRepository { get; }
    }
}
