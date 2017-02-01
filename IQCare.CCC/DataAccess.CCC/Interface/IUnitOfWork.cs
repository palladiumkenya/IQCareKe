using DataAccess.Context.ModuleMaster;
using System;
using DataAccess.CCC.Interface.enrollment;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.CCC.Interface.person;
using DataAccess.CCC.Interface.visit;
using DataAccess.CCC.Repository.Patient;
using DataAccess.CCC.Repository.visit;

namespace DataAccess.CCC.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        
        IModuleRepository ModuleRepository { get; }

        /* LookupContext */
        ILookupRepository LookupRepository { get; }
        ILookupMasterRepository LookupMasterRepository { get; }

        /* person and patient */
        IPersonRepository PersonRepository { get; }
        IPersonLocationRepository PersonLocationRepository { get; }
        IPersonContactRepository PersonContactRepository { get; }
        IPersonRelationshipRepository PersonRelationshipRepository { get; }
        IPatientOvcStatusRepository PatientOvcStatusRepository { get; }
        IPatientMaritalStatusRepository PatientMaritalStatusRepository { get; }
        IPatientPopulationRepository PatientPopulationRepository { get;}
        IPatientTreatmentSupporterRepository PatientTreatmentSupporterRepository { get; }

        /* patient visit */
        IPatientMasterVisitRepository PatientMasterVisitRepository { get; }
        IPatientEncounterRepository PatientEncounterRepository { get; }

        /* Enrollment */
        IPatientEnrollmentRepository PatientEnrollmentRepository { get; }
        IPatientEntryPointRepository PatientEntryPointRepository { get; }
        IPatientIdentifierRepository PatientIdentifierRepository { get; }
    }
}
