using System;
using Config.Core.Interfaces;
using PatientManagement.Core.Interfaces;
using PersonManagement.Core.Interfaces;
using VisitManagement.Core.Interfaces;

namespace Unitofwork.Core.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        
        IServiceAreaRepository ServiceAreaRepository { get; }

        //Person Context Interface
        IPersonRepository PersonRepository { get; }
        IPersonLocationRepository PersonLocationRepository { get; }
        IPersonContactRepository PersonContactRepository { get; }
        IPersonRelationshipRepository PersonRelationshipRepository { get; }

        //Patient Context Interfaces
        IPatientRepository PatientRepository { get; }
        IPatientEnrollmentRepository PatientEnrollmentRepository { get; }
        IPatientMaritalStatusRepository PatientMaritalStatusRepository { get; }
        IPatientOVCStatusRepository PatientOvcStatusRepository { get; }
        IPatientPopulationRepository PatientPopulationRepository { get; }
        IPatientTreatmentSupporterRepository PatientTreatmentSupporterRepository { get; }

        //Visitcontext
        IPatientMasterVisitRepository PatientmasterVisitRepository { get; }
        IPatientEncounterRepository PatientEncounterRepository { get; }
    }
}
