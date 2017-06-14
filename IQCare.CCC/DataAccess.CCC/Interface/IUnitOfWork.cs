using DataAccess.Context.ModuleMaster;
using System;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.CCC.Interface.enrollment;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.CCC.Interface.person;
using DataAccess.CCC.Interface.Patient;
using DataAccess.CCC.Interface.Tb;
using DataAccess.CCC.Interface.visit;
using DataAccess.CCC.Interface.Triage;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.CCC.Repository.person;
using Interface.CCC.Lookup;

namespace DataAccess.CCC.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        
        IModuleRepository ModuleRepository { get; }

        /* LookupContext */
        ILookupRepository LookupRepository { get; }
        ILookupMasterRepository LookupMasterRepository { get; }
        IPatientLookupRepository PatientLookupRepository { get; }
        ILookupPreviousLabs LookupPreviousLabsRepository { get; }
        ILookupFacilityViralLoad LookupFacilityViralLoadRepository { get; }
        IPatientBaselineLookupRepository PatientBaselineLookupRepository { get; }
        ILookupFacilityStatisticsRepository LookupFacilityStatisticsRepository { get; }
        IPatientTreatmentTrackerLookupRepository PatientTreatmentTrackerLookupRepository { get; } 
        IFacilityListRepository FacilityListRepository { get; }
        //ILookupPatientRegimenMap LookupPatientRegimenMapRepository { get; }

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
        IPatientLabTrackerRepository PatientLabTrackerRepository { get; }
        IPatientLabDetailsRepository PatientLabDetailsRepository { get; }

        /* Enrollment */
        IPatientEnrollmentRepository PatientEnrollmentRepository { get; }
        IPatientEntryPointRepository PatientEntryPointRepository { get; }
        IPatientIdentifierRepository PatientIdentifierRepository { get; }

        /*Triage*/
        IPatientVitalsRepository PatientVitalsRepository { get; }
        IPatientFamilyPlanningMethodRepository PatientFamilyPlanningMethodRepository { get; }
        IPatientFamilyPlanningRepository PatientFamilyPlanningRepository { get; }
        IPatientPregnancyIndicatorRepository PatientPregnanacyIndicatorRepository { get; }
        IPatientPregnancyRepository PatientPregnancyRepository { get;}


        /* patient screening */
        IPatientScreeningRepository PatientScreeningRepository { get; }

        /* Baseline */
        IPatientArvHistoryRepository PatientArvHistoryRepository { get;  }
        IPatientHivDiagnosisRepository PatientDiagnosisHivHistoryRepository { get; }
        IPatientDisclosureRepository PatientDisclosureRepository { get; }
        IPatientHivEnrollmentBaselineRepository PatientHivEnrollmentBaselineRepository { get; }
        IPatientTransferInRepository PatientTransferInRepository { get; }
        IPatientBaselineAssessmentRepository PatientBaselineAssessmentRepository { get; }
        IPatientTreatmentInitiationRepository PatientTreatmentInitiationRepository { get;}

        /*Appointment*/
        IPatientAppointmentRepository PatientAppointmentRepository { get; }

        /*Consent*/
        IPatientConsentRepository PatientConsentRepository { get; }

        //TB ICF/IPT
        IPatientIcfRepository PatientIcfRepository { get; }
        IPatientIcfActionRepository PatientIcfActionRepository { get; }
        IPatientIptRepository PatientIptRepository { get; }
        IPatientIptOutcomeRepository PatientIptOutcomeRepository { get; }
        IPatientIptWorkupRepository PatientIptWorkupRepository { get; }

    }
}
