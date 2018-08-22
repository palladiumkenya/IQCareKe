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
using DataAccess.CCC.Interface.assessment;
using DataAccess.CCC.Interface.IL;
using DataAccess.CCC.Interface.Interoperability;
using DataAccess.CCC.Interface.Pharmacy;
using DataAccess.CCC.Repository.assessment;
using DataAccess.CCC.Repository.IL;
using DataAccess.CCC.Repository.Interoperability;
using DataAccess.CCC.Repository.Pharmacy;
using DataAccess.CCC.Interface.Adherence;
using DataAccess.CCC.Repository.Adherence;
using DataAccess.CCC.Interface.HIVEducation;
using DataAccess.CCC.Repository.HIVEducation;

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
        private IPatientSexualHistoryRepository _patientSexualHistoryRepository;
        private IPatientHighRiskRepository _patientHighRiskRepository;
        private IPatientPartnersRepository _patientPartnersRepository;
        /* Patient Interface */
        private IPatientVitalsRepository _patientVitalsRepository;

        /* Modules */
        private IModuleRepository _moduleRepository;

        private IPatientOIRepository _patientOIRepository;
        /* lookupContext */
        private ILookupItemRepository _lookupItemRepository;
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
        private ITestingSummaryStatisticsRepository _testingSummaryStatisticsRepository;
        private IPatientStabilitySummaryRepository _patientStabilitySummaryRepository;
        private IPregnancyOutcomeLookupRepository _pregnancyOutcomeLookupRepository;
        private IIlStatisticsRepository _ilStatisticsRepository;
        private iILMessageStatsRepository _ilMessageStatsRepository;
        private iILMessageViewerRepository _ilMessageViewerRepository;
        private IIlMessengerRepository _ilMessengerRepository;
        private IPersonExtendedLookupRepository _personExtendedLookupRepository;

        /* visit */
        private IPatientMasterVisitRepository _patientMasterVisitRepository;
        private IPatientVisitRepository _patientVisitRepository;
        private IPatientEncounterRepository _patientEncounterRepository;
        private IPatientLabTrackerRepository _patientLabTrackerRepository;
        private IPatientLabOrderRepository _patientLabOrderRepository;
        private IPatientLabDetailsRepository _patientLabDetailsRepository;
        private IPatientLabResultsRepository _patientLabResultsRepository;
        private IPatientWHOStageRepository _patientWhoStageRepository;

        /* Enrollment */
        private IPatientEnrollmentRepository _patientEnrollmentRepository;
        private IPatientEntryPointRepository _patientEntryPointRepository;
        private IPatientIdentifierRepository _patientIdentifierRepository;
        private IServiceAreaIdentifiersRepository _serviceAreaIdentifiersRepository;
        private IIdentifierRepository _identifierRepository;
        private IPatientReEnrollmentRepository _patientReEnrollmentRepository;
        private IHivReConfirmatoryTestRepository _hivReConfirmatoryTestRepository;
        private IPatientArtDistributionRepository _patientArtDistributionRepository;
        private IPersonIdentifierRepository _personIdentifierRepository;
        /* Patient */
        private IPatientRepository _patientRepository;

        /* Triage */
        private IPatientFamilyPlanningMethodRepository _PatientFamilyPlanningMethodRepository;
        private IPatientFamilyPlanningRepository _PatientFamilyPlanningRepository;
        private IPatientPregnancyIndicatorRepository _PatientPregnancyIndicatorRepository;
        private IPatientPregnancyRepository _PatientPregnancyRepository;
        private IPatientScreeningRepository _PatientScreeningRepository;
        private IPatientAdverseEventOutcomeRepository _patientAdverseEventOutcomeRepository;

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
        private IPatientLinkageRepository _patientLinkageRepository;

        /*Appointment*/
        private IPatientAppointmentRepository _patientAppointmentRepository;
        private IBluecardAppointmentRepository _bluecardAppointmentRepository;
        /*Encounter*/
        private IPatientCareEndingRepository _patientCareEndingRepository;
        private IPatientAdherenceAssessmentRepository _patientAdherenceAssessmentRepository;
        private ISocialHistoryRepository _socialHistoryRepository;

        /*Consent*/
        private IPatientConsentRepository _patientConsentRepository;

        //TB ICF/IPT
        private IPatientIcfRepository _patientIcfRepository;
        private IPatientIcfActionRepository _patientIcfActionRepository;
        private IPatientIptRepository _patientIptRepository;
        private IPatientIptOutcomeRepository _patientIptOutcomeRepository;
        private IPatientIptWorkupRepository _patientIptWorkupRepository;

        /*Patient categorization*/
        private IPatientCategorizationRepository _patientCategorizationRepository;

        /*ART Treatment Preparation */
        private IPatientPsychosocialCriteriaRepository _patientPsychosocialCriteriaRepository;
        private IPatientSupportSystemCriteriaRepository _patientSupportSystemCriteriaRepository;

        /*Interoperability*/
        private IPatientMessageRepository _patientMessageRepository;
        private IDrugPrescriptionMessageRepository _drugPrescriptionMessageRepository;
        private IViralLoadMessageRepository _viralLoadMessageRepository;
        private IPatientAppointmentMessageRepository _patientAppointmentMessageRepository;
        private IInteropPlacerTypesRepository _interopPlacerTypesRepository;
        private IInteropPlacerValuesRepository _interopPlacerValuesRepository;
        private IPatientVitalsMessageRepository _patientVitalsMessageRepository;
        IPatientPersonViewRepository _patientPersonViewRepository;
        //pharmacy
        private IPatientPharmacyDispenseRepository _patientPharmacyDispenseRepository;
        private IPharmacyOrderRepository _pharmacyOrderRepository;
        private IDrugRepository _drugRepository;

        //Neonatal
        private IPatientNeonatalRepository _patientNeonatalRepository;
        private IImmunizationHistoryRepository _immunizationHistoryRepository;
        private ITannersStagingRepository _tannersStagingRepository;
        private IRecordTannersStagingRepository _recordTannersStagingRepository;
        private NeonatalRepository _neonatalRepository;
        private MilestonesRepository _milestonesRepository;

        //Adherence
        private IAdherenceRepository _adherenceRepository;
        private IAdherenceScreeningRepository _adherenceScreeningRepository;
        private IAdherencePsychosocialRepository _adherencePsychosocialRepository;
        private IAdherenceDailyRoutineRepository _adherenceDailyRoutineRepository;
        private IAdherenceHIVInfectionRepository _adherenceHIVInfectionRepository;
        private IAdherenceReferralsRepository _adherenceReferralsRepository;

        //Notes
        private IPatientClinicalNotesRepository _patientClinicalNotesRepository;

        //HIV Followup Education
        private IHIVEducationRepository _patientHIVEducationRepository;


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

        public IPatientOIRepository PatientOIRepository
        {
            get
            {
                return _patientOIRepository ??
                 (_patientOIRepository = new PatientOIRepository((GreencardContext)_context));
            }
        }
     
        public ILookupRepository LookupRepository
        {
            get { return _lookupRepository ?? (_lookupRepository = new LookupRepository((LookupContext)_context)); }
        }

        public IPatientMessageRepository PatientMessageRepository
        {
            get
            {
                return _patientMessageRepository ??
                       (_patientMessageRepository = new PatientMessageRepository((LookupContext) _context));
            }
        }

        public IPatientAppointmentMessageRepository PatientAppointmentMessageRepository
        {
            get
            {
                return _patientAppointmentMessageRepository ?? (_patientAppointmentMessageRepository =
                           new PatientAppointmentMessageRepository((LookupContext) _context));
            }
        }

        public IPregnancyOutcomeLookupRepository PregnancyOutcomeLookupRepository
        {
            get
            {
                return _pregnancyOutcomeLookupRepository ?? (_pregnancyOutcomeLookupRepository =
                           new PregnancyOutcomeLookupRepository((LookupContext) _context));
            }
        }

        public IInteropPlacerTypesRepository InteropPlacerTypesRepository
        {
            get
            {
                return _interopPlacerTypesRepository ?? (_interopPlacerTypesRepository =
                           new InteropPlacerTypeRepository((GreencardContext) _context));
            }
        }

        public IInteropPlacerValuesRepository InteropPlacerValuesRepository
        {
            get
            {
                return _interopPlacerValuesRepository ?? (_interopPlacerValuesRepository =
                           new InteropPlacerValuesRepository((GreencardContext) _context));
            }
        }

        public IDrugPrescriptionMessageRepository DrugPrescriptionMessageRepository
        {
            get
            {
                return _drugPrescriptionMessageRepository ?? (_drugPrescriptionMessageRepository =
                           new DrugPrescriptionMessageRepository((LookupContext) _context));
            }
        }

        public IViralLoadMessageRepository ViralLoadMessageRepository
        {
            get
            {
                return _viralLoadMessageRepository ??
                       (_viralLoadMessageRepository = new ViralLoadMessageRepository((LookupContext)_context));
            }
        }

        public IPatientPharmacyDispenseRepository PatientPharmacyDispenseRepository
        {
            get
            {
                return _patientPharmacyDispenseRepository ?? 
                    (_patientPharmacyDispenseRepository = new PatientPharmacyDispenseRepository((GreencardContext)_context));
            }
        }

        public IPharmacyOrderRepository PharmacyOrderRepository
        {
            get
            {
                return _pharmacyOrderRepository ??
                       (_pharmacyOrderRepository = new PharmacyOrderRepository((GreencardContext)_context));
            }
        }

        public IDrugRepository DrugRepository
        {
            get
            {
                return _drugRepository ??
                       (_drugRepository = new DrugRepository((GreencardContext)_context));
            }
        }

        public IPatientServiceEnrollmentLookupRepository PatientServiceEnrollmentLookupRepository
        {
            get
            {
                return _patientServiceEnrollmentLookupRepository ??
                       (_patientServiceEnrollmentLookupRepository = new PatientServiceEnrollmentLookupRepository((LookupContext) _context));
            }
        }
        public IPatientSexualHistoryRepository PatientSexualHistoryRepository
        {
            get
            {
                return _patientSexualHistoryRepository ??
                       (_patientSexualHistoryRepository =
                           new PatientSexualHistoryRepository((GreencardContext)_context));
            }
        }

        public IPatientPartnersRepository PatientPartnersRepository
        {
            get
            {
                return _patientPartnersRepository ??
                    (_patientPartnersRepository = new PatientPartnersRepository((GreencardContext)_context));
            }
        }

        public IPatientHighRiskRepository PatientHighRiskRepository
        {
            get
            {
                return _patientHighRiskRepository ??
                    (_patientHighRiskRepository = new PatientHighRiskRepository((GreencardContext)_context));
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

        public IIlMessengerRepository IlMessengerRepository
        {
            get
            {
                return _ilMessengerRepository ??
                       (_ilMessengerRepository = new IlMessengerRepository((LookupContext) _context));
            }
        }

        public IPersonExtendedLookupRepository PersonExtendedLookupRepository
        {
            get {
                return _personExtendedLookupRepository ??(_personExtendedLookupRepository=new PersonExtendedLookupRepository((LookupContext)_context));
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

        public IPatientWHOStageRepository PatientWhoStageRepository { get
            {
                return _patientWhoStageRepository ?? (_patientWhoStageRepository =
                           new PatientWhoStageRepository((GreencardContext) _context));
            } }

        public IPatientEnrollmentRepository PatientEnrollmentRepository
        {
            get { return _patientEnrollmentRepository ?? (_patientEnrollmentRepository = new PatientEnrollmentRepository((GreencardContext)_context)); }
        }

       
       public IPatientPersonViewRepository PatientPersonViewRepository
        {
            get
            {
                 return _patientPersonViewRepository ?? (_patientPersonViewRepository = new PatientPersonViewRepository((GreencardContext)_context)); 
            }
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

        public IHivReConfirmatoryTestRepository HivReConfirmatoryTestRepository { get
            {
                return _hivReConfirmatoryTestRepository ?? (_hivReConfirmatoryTestRepository =
                           new HivReConfirmatoryTestRepository((GreencardContext) _context));
            } }

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

        public IPatientLinkageRepository PatientLinkageRepository
        {
            get
            {
                return _patientLinkageRepository ?? (_patientLinkageRepository =
                           new PatientLinkageRepository((GreencardContext) _context));
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

        public IIlStatisticsRepository IlStatisticsRepository
        {
            get {
                return _ilStatisticsRepository??(_ilStatisticsRepository= new IlStatisticsRepository((LookupContext)_context));

            }
        }

        public iILMessageStatsRepository IIlMessageStatsRepository
        {
            get
            {
                return _ilMessageStatsRepository ??
                       (_ilMessageStatsRepository = new ILMessageStatsRepository((LookupContext) _context));
            }
        }

        public iILMessageViewerRepository IIlMessageViewerRepository
        {
            get
            {
                return _ilMessageViewerRepository ??
                       (_ilMessageViewerRepository = new ILMessageViewerRepository((LookupContext) _context));
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

        public ITestingSummaryStatisticsRepository TestingSummaryStatisticsRepository { get
            {
                return _testingSummaryStatisticsRepository ?? (_testingSummaryStatisticsRepository =
                           new TestingSummaryStatisticsRepository((LookupContext) _context));
            } }

        public IPatientStabilitySummaryRepository PatientStabilitySummaryRepository { get
            {
                return _patientStabilitySummaryRepository ?? (_patientStabilitySummaryRepository =
                           new PatientStabilitySummaryRepository((LookupContext) _context));
            } }

        public IPersonGreenCardLookupRepository PersonGreenCardLookupRepository
        {
            get
            {
                return _personGreenCardLookupRepository ??
                       (_personGreenCardLookupRepository =
                           new PersonGreenCardLookupRepository((GreencardContext) _context));
            }
        }

        public IPatientVitalsMessageRepository PatientVitalsMessageRepository
        {
            get
            {
                return _patientVitalsMessageRepository ?? (_patientVitalsMessageRepository =
                           new PatientVitalsMessageRepository((LookupContext) _context));
            }
        }

        public IPatientCategorizationRepository PatientCategorizationRepository
        {
            get { return _patientCategorizationRepository ?? (_patientCategorizationRepository = new PatientCategorizationRepository((GreencardContext)_context)); }
        }

        /*ART Treatment Preparation */
        public IPatientPsychosocialCriteriaRepository PatientPsychosocialCriteriaRepository
        {
            get { return  _patientPsychosocialCriteriaRepository ?? (_patientPsychosocialCriteriaRepository=new PatientPsychosocialCriteriaRepository((GreencardContext)_context)); }
        }

        public IPatientSupportSystemCriteriaRepository PatientSupportSystemCriteriaRepository
        {
            get { return _patientSupportSystemCriteriaRepository ?? (_patientSupportSystemCriteriaRepository = new PatientSupportSystemCriteriaRepository((GreencardContext)_context)); }
        }

        public IPatientArtDistributionRepository PatientArtDistributionRepository
        {
            get { return _patientArtDistributionRepository ?? (_patientArtDistributionRepository= new PatientArtDistributionRepository((GreencardContext)_context)); }
        }

        public IPersonIdentifierRepository PersonIdentifierRepository
        {
            get
            {
                return _personIdentifierRepository ?? (_personIdentifierRepository =
                           new PersonIdentifierRepository((GreencardContext) _context));
            }
        }

        public IPatientAdverseEventOutcomeRepository PatientAdverseEventOutcomeRepository
        {
            get
            {
                return _patientAdverseEventOutcomeRepository ?? (_patientAdverseEventOutcomeRepository = new PatientAdverseEventOutcomeRepository((GreencardContext)_context));
            }
        }

        public IPatientNeonatalRepository PatientNeonatalRepository
        {
            get { return _patientNeonatalRepository ?? (_patientNeonatalRepository = new PatientNeonatalRepository((GreencardContext)_context)); }
        }
        public IMilestonesRepository MilestonesRepository
        {
            get { return _milestonesRepository ?? (_milestonesRepository = new MilestonesRepository((GreencardContext)_context)); }
        }
        public IImmunizationHistoryRepository ImmunizationHistoryRepository
        {
            get { return _immunizationHistoryRepository ?? (_immunizationHistoryRepository = new ImmunizationHistoryRepository((GreencardContext)_context)); }
        }

        public ITannersStagingRepository TannersStagingRepository
        {
            get { return _tannersStagingRepository ?? (_tannersStagingRepository= new TannersStagingRepository((GreencardContext)_context)); }
        }
        public IRecordTannersStagingRepository RecordTannersStagingRepository
        {
            get { return _recordTannersStagingRepository ?? (_recordTannersStagingRepository = new RecordTannersStagingRepository((GreencardContext)_context)); }
        }

        public ISocialHistoryRepository SocialHistoryRepository
        {
            get
            {
                return _socialHistoryRepository ??(_socialHistoryRepository = new SocialHistoryRepository((GreencardContext)_context));
            }
        }
        public INeonatalRepository NeonatalRepository
        {
            get { return _neonatalRepository ?? (_neonatalRepository = new NeonatalRepository((GreencardContext)_context)); }
        }

        public IAdherenceRepository AdherenceRepository
        {
            get { return _adherenceRepository ?? (_adherenceRepository = new AdherenceRepository((GreencardContext)_context)); }
        }
        public IAdherenceScreeningRepository AdherenceScreeningRepository
        {
            get { return _adherenceScreeningRepository ?? (_adherenceScreeningRepository = new AdherenceScreeningRepository((GreencardContext)_context)); }
        }
        public IAdherencePsychosocialRepository AdherencePsychosocialRepository
        {
            get { return _adherencePsychosocialRepository ?? (_adherencePsychosocialRepository = new AdherencePsychosocialRepository((GreencardContext)_context)); }
        }
        public IAdherenceDailyRoutineRepository AdherenceDailyRoutineRepository
        {
            get { return _adherenceDailyRoutineRepository ?? (_adherenceDailyRoutineRepository = new AdherenceDailyRoutineRepository((GreencardContext)_context)); }
        }
        public IAdherenceHIVInfectionRepository AdherenceHIVInfectionRepository
        {
            get { return _adherenceHIVInfectionRepository ?? (_adherenceHIVInfectionRepository = new AdherenceHIVInfectionRepository((GreencardContext)_context)); }
        }
        public IAdherenceReferralsRepository AdherenceReferralsRepository
        {
            get { return _adherenceReferralsRepository ?? (_adherenceReferralsRepository = new AdherenceReferralsRepository((GreencardContext)_context)); }
        }
        public IPatientClinicalNotesRepository PatientClinicalNotesRepository
        {
            get { return _patientClinicalNotesRepository ?? (_patientClinicalNotesRepository = new PatientClinicalNotesRepository((GreencardContext)_context)); }
        }

        public IHIVEducationRepository PatientHIVEducationFollowupRepository
        {
            get { return _patientHIVEducationRepository ?? (_patientHIVEducationRepository = new PatientHIVEducationFollowupRepository((GreencardContext)_context)); }
        }

        public ILookupItemRepository LookupItemRepository => _lookupItemRepository ?? (_lookupItemRepository = new LookupItemRepository((LookupContext)_context));

        public object HIVEducationFollowup { get; set; }

        // public IPatientAdverseEventOutcomeRepository PatientAdverseEventOutcomeRepository => throw new NotImplementedException();

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
