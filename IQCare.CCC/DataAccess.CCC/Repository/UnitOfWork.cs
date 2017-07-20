using System;
using System.Data.Entity;
using DataAccess.CCC.Context;
using DataAccess.Context;
using DataAccess.Context.ModuleMaster;
using DataAccess.CCC.Interface;
using DataAccess.CCC.Interface.enrollment;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.CCC.Interface.person;
using DataAccess.CCC.Interface.Patient;
using DataAccess.CCC.Interface.visit;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.CCC.Repository.Lookup;
using DataAccess.CCC.Repository.person;
using DataAccess.CCC.Repository.Patient;
using DataAccess.CCC.Repository.visit;
using DataAccess.CCC.Repository.Encounter;
using DataAccess.CCC.Repository.Enrollment;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.CCC.Interface.Tb;
using DataAccess.CCC.Repository.Baseline;
using DataAccess.CCC.Repository.Tb;
using PatientLabResultsRepository = DataAccess.CCC.Repository.Encounter.PatientLabResultsRepository;
using DataAccess.CCC.Interface.Triage;
using DataAccess.CCC.Repository.Triage;
using DataAccess.CCC.Repository.Screening;

namespace DataAccess.CCC.Repository
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private BaseContext _context;

                /* Person Interface */
        private IPersonRepository _personRepository;
        private IPersonLocationRepository _personLocationRepository;
        private IPersonContactRepository _personContactRepository;
        private IPersonRelationshipRepository _personRelationshipRepository;
        private IPatientMaritalStatusRepository _patientMaritalStatusRepository;
        private IPatientOvcStatusRepository _patientOvcStatusRepository;
        private IPatientPopulationRepository _patientPopulationRepository;
        private IPatientTreatmentSupporterRepository _patientTreatmentSupporterRepository;

        /* Patient Interface */
        private IPatientVitalsRepository _patientVitalsRepository;

        /* Modules */
        private IModuleRepository _moduleRepository;

        /* lookupContext */
        private ILookupRepository _lookupRepository;
        private ILookupMasterRepository _lookupMasterRepository;
        private IPatientLookupRepository _patientLookupRepository;
        private ILookupLabs _lookupLabsRepository;
        private ILookupParameter _lookupParameterRepository;
        private ILookupFacility _lookupFacilityRepository;
        private ILookupPreviousLabs _lookupPreviousLabsRepository;
        private ILookupFacilityViralLoad _lookupFacilityViralLoadRepository;
        private IPersonLookUpRepository _personLookUpRepository;
        private IPersonContactLookUpRepository _personContactLookUpRepository;
        private IPatientBaselineLookupRepository _patientBaselineLookupRepository;
        private ILookupCounty _lookupCounty;
        private IPatientServiceEnrollmentLookupRepository _patientServiceEnrollmentLookupRepository;
        private IPatientTreatmentSupporterLookupRepository _patientTreatmentSupporterLookupRepository;
        private ILookupFacilityStatisticsRepository _lookupFacilityStatisticsRepository;
        private IPatientTreatmentTrackerLookupRepository _patientTreatmentTrackerLookupRepository;
        private IPersonGreenCardLookupRepository _personGreenCardLookupRepository;
        private IFacilityListRepository _facilityListRepository;
        private IPatientRegistrationLookupRepository _patientRegistrationLookupRepository;

        /* visit */
        private IPatientMasterVisitRepository _patientMasterVisitRepository;
        private IPatientVisitRepository _patientVisitRepository;
        private IPatientEncounterRepository _patientEncounterRepository;
        private IPatientLabTrackerRepository _patientLabTrackerRepository;
        private IPatientLabOrderRepository _patientLabOrderRepository;
        private IPatientLabDetailsRepository _patientLabDetailsRepository;
        private IPatientLabResultsRepository _patientLabResultsRepository;

        /* Enrollment */
        private IPatientEnrollmentRepository _patientEnrollmentRepository;
        private IPatientEntryPointRepository _patientEntryPointRepository;
        private IPatientIdentifierRepository _patientIdentifierRepository;
        private IServiceAreaIdentifiersRepository _serviceAreaIdentifiersRepository;
        private IIdentifierRepository _identifierRepository;
        private IPatientReEnrollmentRepository _patientReEnrollmentRepository;

        /* Patient */
        private IPatientRepository _patientRepository;

        /* Triage */
        private IPatientFamilyPlanningMethodRepository _PatientFamilyPlanningMethodRepository;
        private IPatientFamilyPlanningRepository _PatientFamilyPlanningRepository;
        private IPatientPregnancyIndicatorRepository _PatientPregnancyIndicatorRepository;
        private IPatientPregnancyRepository _PatientPregnancyRepository;
        private IPatientScreeningRepository _PatientScreeningRepository;

        /*Baseline*/
        // private IPatientDisclosureRepository _patientDisclosureRepository;
        private IPatientArvHistoryRepository _patientArvHistoryRepository;
        private IPatientHivDiagnosisRepository _patientDiagnosisHivHistoryRepository;
        private IPatientDisclosureRepository _patientDisclosureRepository;
        private IINHProphylaxisRepository _inhProphylaxisRepository;
        private IPatientHivEnrollmentBaselineRepository _patientHivEnrollmentBaselineRepository;
        private IPatientTransferInRepository _patientTransferInRepository;
        private IPatientBaselineAssessmentRepository _patientBaselineAssessmentRepository;
        private IPatientVaccinationRepository _patientVaccinationRepository;
        private IPatientHvTestingRepository _patientHivTestingRepository;
        private IPatientTreatmentInitiationRepository _patientTreatmentInitiationRepository;

        /*Appointment*/
        private IPatientAppointmentRepository _patientAppointmentRepository;
        private IBluecardAppointmentRepository _bluecardAppointmentRepository;
        /*Encounter*/
        private IPatientCareEndingRepository _patientCareEndingRepository;
        private IPatientAdherenceAssessmentRepository _patientAdherenceAssessmentRepository;

        /*Consent*/
        private IPatientConsentRepository _patientConsentRepository;

        //TB ICF/IPT
        private IPatientIcfRepository _patientIcfRepository;
        private IPatientIcfActionRepository _patientIcfActionRepository;
        private IPatientIptRepository _patientIptRepository;
        private IPatientIptOutcomeRepository _patientIptOutcomeRepository;
        private IPatientIptWorkupRepository _patientIptWorkupRepository;

        public UnitOfWork(BaseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("missing context");
            }
            _context = context;
        }

        public DbContext Context { get { return _context; } }

        public IModuleRepository ModuleRepository
        {
            get
            {
                return _moduleRepository ?? (_moduleRepository = new ModuleRepository((ModuleContext)_context));
            }
        }

     
        public ILookupRepository LookupRepository
        {
            get { return _lookupRepository ?? (_lookupRepository = new LookupRepository((LookupContext)_context)); }
        }

        public IPatientServiceEnrollmentLookupRepository PatientServiceEnrollmentLookupRepository
        {
            get
            {
                return _patientServiceEnrollmentLookupRepository ??
                       (_patientServiceEnrollmentLookupRepository = new PatientServiceEnrollmentLookupRepository((LookupContext) _context));
            }
        }

        public IPatientTreatmentSupporterLookupRepository PatientTreatmentSupporterLookupRepository
        {
            get
            {
                return _patientTreatmentSupporterLookupRepository ??
                       (_patientTreatmentSupporterLookupRepository =
                           new PatientTreatmentSupporterLookupRepository((LookupContext) _context));
            }
        }

        public ILookupMasterRepository LookupMasterRepository
        {
            get
            {
                return _lookupMasterRepository ?? (_lookupMasterRepository = new LookupMasterRepository((LookupContext)_context));
            }
        }
        public ILookupLabs LookupLabsRepository
        {
            get { return _lookupLabsRepository ?? (_lookupLabsRepository = new LookupLabsRepository((LookupContext)_context)); }
        }
        public ILookupParameter LookupParameterRepository
        {
            get { return _lookupParameterRepository ?? (_lookupParameterRepository = new LookupParameterRepository((LookupContext)_context)); }
        }
        public ILookupFacility LookupFacilityRepository
        {
            get { return _lookupFacilityRepository ?? (_lookupFacilityRepository = new LookupFacilityRepository((LookupContext)_context)); }
        }
        public ILookupPreviousLabs LookupPreviousLabsRepository
        {
            get
            {
                return _lookupPreviousLabsRepository ?? (_lookupPreviousLabsRepository = new LookupPreviousLabsRepository((LookupContext)_context));
            }
        }
        public ILookupFacilityViralLoad LookupFacilityViralLoadRepository
        {
            get
            {
                return _lookupFacilityViralLoadRepository ?? (_lookupFacilityViralLoadRepository = new LookupFacilityViralLoadRepository((LookupContext)_context));
            }
        }

        public ILookupCounty LookupCountyRepository
        {
            get
            {
                return _lookupCounty??(_lookupCounty=new LookupCountyRepository((LookupContext)_context));
            }
        }

        public ILookupFacilityStatisticsRepository LookupFacilityStatisticsRepository
        {
            get
            {
                return _lookupFacilityStatisticsRepository ??
                       (_lookupFacilityStatisticsRepository =
                           new LookupFacilityStatisticsRepository((LookupContext) _context));
            }
        }

        public IPersonRepository PersonRepository
        {
            get { return _personRepository ?? (_personRepository = new PersonRepository((PersonContext)_context)); }
        }

        public IPersonContactRepository PersonContactRepository
        {
            get
            {
                return _personContactRepository ??
                       (_personContactRepository = new PersonContactRepository((PersonContext)_context));
            }
        }

        public IPersonLocationRepository PersonLocationRepository
        {
            get
            {
                return _personLocationRepository ??
                       (_personLocationRepository = new PersonLocationRepository((PersonContext)_context));
            }
        }

        public IPersonRelationshipRepository PersonRelationshipRepository
        {
            get { return _personRelationshipRepository ?? (_personRelationshipRepository = new PersonRelationshipRepository((PersonContext)_context)); }
        }

        public IPatientOvcStatusRepository PatientOvcStatusRepository
        {
            get { return _patientOvcStatusRepository ?? (_patientOvcStatusRepository = new PatientOVCStatusRepository((PersonContext)_context)); }
        }

        public IPatientMaritalStatusRepository PatientMaritalStatusRepository
        {
            get { return _patientMaritalStatusRepository ?? (_patientMaritalStatusRepository = new PatientMaritalStatusRepository((PersonContext)_context)); }
        }

        public IPatientPopulationRepository PatientPopulationRepository
        {
            get { return _patientPopulationRepository ?? (_patientPopulationRepository = new PatientPopulationRepository((PersonContext)_context)); }
        }

        public IPatientTreatmentSupporterRepository PatientTreatmentSupporterRepository
        {
            get
            {
                return _patientTreatmentSupporterRepository ?? (_patientTreatmentSupporterRepository = new PatientTreatmentSupporterRepository((PersonContext)_context));
            }
        }
        public IPatientMasterVisitRepository PatientMasterVisitRepository
        {
            get
            {
                return _patientMasterVisitRepository ?? (_patientMasterVisitRepository = new PatientMasterVisitRepository((GreencardContext)_context));
            }
        }
        public IPatientVisitRepository PatientVisitRepository
        {
            get
            {
                return _patientVisitRepository ?? (_patientVisitRepository = new PatientVisitRepository((GreencardContext)_context));
            }
        }
        public IPatientEncounterRepository PatientEncounterRepository
        {
            get { return _patientEncounterRepository ?? (_patientEncounterRepository = new PatientEncounterRepository((GreencardContext)_context)); }
        }
        public IPatientLabTrackerRepository PatientLabTrackerRepository
        {
            get { return _patientLabTrackerRepository ?? (_patientLabTrackerRepository = new PatientLabTrackerRepository((GreencardContext)_context)); }
        }
        public IPatientLabOrderRepository PatientLabOrderRepository
        {
            get { return _patientLabOrderRepository ?? (_patientLabOrderRepository = new PatientLabOrderRepository((GreencardContext)_context)); }
        }
        public IPatientLabDetailsRepository PatientLabDetailsRepository
        {
            get { return _patientLabDetailsRepository ?? (_patientLabDetailsRepository = new PatientLabDetailsRepository((GreencardContext)_context)); }
        }
        public IPatientLabResultsRepository PatientLabResultsRepository
        {
            get { return _patientLabResultsRepository ?? (_patientLabResultsRepository = new PatientLabResultsRepository((GreencardContext)_context)); }
        }

        public IPatientEnrollmentRepository PatientEnrollmentRepository
        {
            get { return _patientEnrollmentRepository ?? (_patientEnrollmentRepository = new PatientEnrollmentRepository((GreencardContext)_context)); }
        }

        public IPatientRepository PatientRepository
        {
            get { return _patientRepository ?? (_patientRepository = new Patient.PatientRepository((GreencardContext)_context)); }
        }

        public IPatientIdentifierRepository PatientIdentifierRepository
        {
            get { return _patientIdentifierRepository ?? (_patientIdentifierRepository = new PatientIdentifierRepository((GreencardContext)_context)); }
        }

        public IServiceAreaIdentifiersRepository ServiceAreaIdentifiersRepository
        {
            get
            {
                return _serviceAreaIdentifiersRepository ?? (_serviceAreaIdentifiersRepository =
                           new ServiceAreaIdentifiersRepository((GreencardContext) _context));
            }
        }

        public IIdentifierRepository IdentifierRepository
        {
            get
            {
                return _identifierRepository ?? (_identifierRepository =
                           new IdentifierRepository((GreencardContext) _context));
            }
        }

        public IPatientReEnrollmentRepository PatientReEnrollmentRepository
        {
            get
            {
                return _patientReEnrollmentRepository ?? (_patientReEnrollmentRepository =
                           new PatientReEnrollmentRepository((GreencardContext) _context));
            }
        }
        public IPatientCareEndingRepository PatientCareEndingRepository
        {
            get
            {
                return _patientCareEndingRepository ??
                       (_patientCareEndingRepository = new PatientCareEndingRepository((GreencardContext)_context));
            }
        }

        public IPatientAdherenceAssessmentRepository PatientAdherenceAssessmentRepository
        {
            get
            {
                return _patientAdherenceAssessmentRepository ??
                       (_patientAdherenceAssessmentRepository =
                           new PatientAdherenceAssessmentRepository((GreencardContext) _context));
            }
        }

        public IPatientEntryPointRepository PatientEntryPointRepository
        {
            get { return _patientEntryPointRepository ?? (_patientEntryPointRepository = new PatientEntrypointRepository((GreencardContext)_context)); }
        }

        public IPatientDisclosureRepository PatientDisclosureRepository
        {
            get { return _patientDisclosureRepository ?? (_patientDisclosureRepository = new PatientDisclosureRepository((GreencardContext)_context)); }
        }

        public IINHProphylaxisRepository INHProphylaxisRepository
        {
            get { return _inhProphylaxisRepository ?? (_inhProphylaxisRepository = new INHProphylaxisRepository((GreencardContext)_context)); }
        }

        public IPatientVaccinationRepository PatientVaccinationRepository
        {
            get { return _patientVaccinationRepository ?? (_patientVaccinationRepository = new PatientVaccinationRepository((GreencardContext)_context)); }
        }

        public IPatientVitalsRepository PatientVitalsRepository
        {
            get { return _patientVitalsRepository ?? (_patientVitalsRepository = new PatientVitalsRepository((GreencardContext)_context)); }
        }

        public IPatientArvHistoryRepository PatientArvHistoryRepository
        {
            get { return _patientArvHistoryRepository ?? (_patientArvHistoryRepository = new PatientArvHistoryRepository((GreencardContext)_context)); }
        }

        public IPatientHivDiagnosisRepository PatientDiagnosisHivHistoryRepository
        {
            get
            {
                return _patientDiagnosisHivHistoryRepository ?? (_patientDiagnosisHivHistoryRepository = new PatientHivDiagnosisRepository((GreencardContext)_context));
            }
        }

        public IPatientHivEnrollmentBaselineRepository PatientHivEnrollmentBaselineRepository
        {
            get
            {
                return _patientHivEnrollmentBaselineRepository ?? (_patientHivEnrollmentBaselineRepository = new PatientHivEnrollmentBaselineRepository((GreencardContext)_context));
            }
        }

        public IPatientTransferInRepository PatientTransferInRepository
        {
            get { return _patientTransferInRepository ?? (_patientTransferInRepository = new PatientTransferInRepository((GreencardContext)_context)); }
        }

        public IPatientBaselineAssessmentRepository PatientBaselineAssessmentRepository
        {
            get { return _patientBaselineAssessmentRepository ?? (_patientBaselineAssessmentRepository = new PatientBaselineAssessmentRepository((GreencardContext)_context)); }
        }

        public IPatientLookupRepository PatientLookupRepository
        {
            get { return _patientLookupRepository ?? (_patientLookupRepository = new PatientLookupRepository((LookupContext)_context)); }
        }

        public IPersonLookUpRepository PersonLookUpRepository
        {
            get
            {
                return _personLookUpRepository ??
                       (_personLookUpRepository = new PersonLookUpRepository((LookupContext)_context));
            }
        }

        public IPersonContactLookUpRepository PersonContactLookUpRepository
        {
            get
            {
                return _personContactLookUpRepository ??
                       (_personContactLookUpRepository = new PersonContactLookUpRepository((LookupContext)_context));
            }
        }

        public IPatientAppointmentRepository PatientAppointmentRepository
        {
            get { return _patientAppointmentRepository ?? (_patientAppointmentRepository = new PatientAppointmentRepository((GreencardContext)_context)); }
        }

        public IBluecardAppointmentRepository BluecardAppointmentRepository
        {
            get { return _bluecardAppointmentRepository ?? (_bluecardAppointmentRepository = new BluecardAppointmentRepository((GreencardContext)_context)); }
        }

        public IPatientTreatmentInitiationRepository PatientTreatmentInitiationRepository
        {
            get
            {
                return _patientTreatmentInitiationRepository ??
                       (_patientTreatmentInitiationRepository =
                           new PatientTreatmentInitiationRepository((GreencardContext)_context));
            }
        }
        public IPatientHvTestingRepository PatientHivTestingRepository
        {
            get { return _patientHivTestingRepository ?? (_patientHivTestingRepository = new PatientHivTestingRepository((GreencardContext)_context)); }
        }

        public IPatientBaselineLookupRepository PatientBaselineLookupRepository
        {
            get { return _patientBaselineLookupRepository ?? (_patientBaselineLookupRepository = new PatientBaselineLookupRepository((LookupContext)_context)); }
        }

        public IPatientConsentRepository PatientConsentRepository
        {
            get { return _patientConsentRepository ?? (_patientConsentRepository = new PatientConsentRepository((GreencardContext)_context)); }
        }

        public IPatientFamilyPlanningMethodRepository PatientFamilyPlanningMethodRepository
        {
            get { return _PatientFamilyPlanningMethodRepository ?? (_PatientFamilyPlanningMethodRepository = new PatientFamilyPlanningMethodRepsotory((GreencardContext)_context)); }
        }
        public IPatientIcfRepository PatientIcfRepository
        {
            get { return _patientIcfRepository ?? (_patientIcfRepository = new PatientIcfRepository((GreencardContext)_context)); }
        }
        public IPatientIcfActionRepository PatientIcfActionRepository
        {
            get { return _patientIcfActionRepository ?? (_patientIcfActionRepository = new PatientIcfActionRepository((GreencardContext)_context)); }
        }
        public IPatientIptRepository PatientIptRepository
        {
            get { return _patientIptRepository ?? (_patientIptRepository = new PatientIptRepository((GreencardContext)_context)); }
        }
        public IPatientIptOutcomeRepository PatientIptOutcomeRepository
        {
            get { return _patientIptOutcomeRepository ?? (_patientIptOutcomeRepository = new PatientIptOutcomeRepository((GreencardContext)_context)); }
        }
        public IPatientIptWorkupRepository PatientIptWorkupRepository
        {
            get { return _patientIptWorkupRepository ?? (_patientIptWorkupRepository = new PatientIptWorkupRepository((GreencardContext)_context)); }
        }

        public IPatientFamilyPlanningRepository PatientFamilyPlanningRepository
        {
            get
            {
                return _PatientFamilyPlanningRepository ?? (_PatientFamilyPlanningRepository = new PatientFamilyPlanningRepository((GreencardContext)_context));
            }
        }

        public IPatientPregnancyIndicatorRepository PatientPregnanacyIndicatorRepository
        {
            get
            {
                return _PatientPregnancyIndicatorRepository ?? (_PatientPregnancyIndicatorRepository = new PatientPregnancyIndicatorRepository((GreencardContext)_context));
            }
        }

        public IPatientPregnancyRepository PatientPregnancyRepository
        {
            get
            {
                return _PatientPregnancyRepository ?? (_PatientPregnancyRepository = new PatientPregnancyRepository((GreencardContext)_context));
            }
        }

        public IPatientScreeningRepository PatientScreeningRepository
        {
            get
            {
                return _PatientScreeningRepository ?? (_PatientScreeningRepository = new PatientScreeningRepository((GreencardContext)_context));
            }
        }

        public IPatientTreatmentTrackerLookupRepository PatientTreatmentTrackerLookupRepository
        {
            get
            {
                return _patientTreatmentTrackerLookupRepository??(_patientTreatmentTrackerLookupRepository=new PatientTreatmentTrackerLookupRepository((LookupContext)_context));
            }
        }

        public IFacilityListRepository FacilityListRepository
        {
            get
            {
                return _facilityListRepository ?? (_facilityListRepository =
                           new FacilityListRepository((LookupContext) _context));
            }
        }

        public IPatientRegistrationLookupRepository PatientRegistrationLookupRepository
        {
            get
            {
                return _patientRegistrationLookupRepository ?? (_patientRegistrationLookupRepository =
                           new PatientRegistrationLookupRepository((LookupContext) _context));
            }
        }

        public IPersonGreenCardLookupRepository PersonGreenCardLookupRepository
        {
            get
            {
                return _personGreenCardLookupRepository ??
                       (_personGreenCardLookupRepository =
                           new PersonGreenCardLookupRepository((GreencardContext) _context));
            }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }


        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
