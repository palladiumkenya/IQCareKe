using System;
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
using DataAccess.CCC.Repository.Enrollment;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.CCC.Repository.Baseline;
using DataAccess.CCC.Repository.Encounter;
using PatientLabOrderRepository = DataAccess.CCC.Repository.visit.PatientLabOrderRepository;

namespace DataAccess.CCC.Repository
{
    public class UnitOfWork : IUnitOfWork
    {  
        private readonly BaseContext _context;

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
        private ILookupFacility _lookupFacilityRepository;
        private ILookupPreviousLabs _lookupPreviousLabsRepository;
        private IPersonLookUpRepository _personLookUpRepository;
        private IPersonContactLookUpRepository _personContactLookUpRepository;
        /* visit */
        private IPatientMasterVisitRepository _patientMasterVisitRepository;
        private IPatientEncounterRepository _patientEncounterRepository;
        private IPatientLabTrackerRepository _patientLabTrackerRepository;
        private IPatientLabOrderRepository _patientLabOrderRepository;

        /* Enrollment */
        private IPatientEnrollmentRepository _patientEnrollmentRepository;
        private IPatientEntryPointRepository _patientEntryPointRepository;
        private IPatientIdentifierRepository _patientIdentifierRepository;
        /* Patient */
        private IPatientRepository _patientRepository;

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
        /*Encounter*/
        private IPatientCareEndingRepository _patientCareEndingRepository;

        public UnitOfWork(BaseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            _context = context;   
        }

        public IModuleRepository ModuleRepository
        {
            get
            {
                return _moduleRepository ?? (_moduleRepository = new ModuleRepository((ModuleContext)_context));
            }
        }

        public ILookupRepository LookupRepository
        {
            get { return _lookupRepository ?? (_lookupRepository = new LookupRepository((LookupContext) _context)); }
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
        public IPersonRepository PersonRepository
        {
            get { return _personRepository ?? (_personRepository = new PersonRepository((PersonContext) _context)); }
        }

        public IPersonContactRepository PersonContactRepository
        {
            get
            {
                return _personContactRepository ??
                       (_personContactRepository = new PersonContactRepository((PersonContext) _context));
            }
        }

        public IPersonLocationRepository PersonLocationRepository
        {
            get
            {
                return _personLocationRepository ??
                       (_personLocationRepository = new PersonLocationRepository((PersonContext) _context));
            }
        }

        public IPersonRelationshipRepository PersonRelationshipRepository
        {
            get { return _personRelationshipRepository??(_personRelationshipRepository=new PersonRelationshipRepository((PersonContext)_context));}
        }

        public IPatientOvcStatusRepository PatientOvcStatusRepository
        {
            get {  return _patientOvcStatusRepository??(_patientOvcStatusRepository=new PatientOVCStatusRepository((PersonContext)_context));  }
        }

        public IPatientMaritalStatusRepository PatientMaritalStatusRepository
        {
            get { return _patientMaritalStatusRepository??(_patientMaritalStatusRepository=new PatientMaritalStatusRepository((PersonContext)_context));}
        }

        public IPatientPopulationRepository PatientPopulationRepository
        {
            get { return _patientPopulationRepository??(_patientPopulationRepository=new PatientPopulationRepository((PersonContext)_context));}
        }

        public IPatientTreatmentSupporterRepository PatientTreatmentSupporterRepository
        {
            get
            {
                return _patientTreatmentSupporterRepository ??(_patientTreatmentSupporterRepository = new PatientTreatmentSupporterRepository((PersonContext) _context));
            }
        }
        public IPatientMasterVisitRepository PatientMasterVisitRepository
        {
            get
            {
                return _patientMasterVisitRepository ?? (_patientMasterVisitRepository = new PatientMasterVisitRepository((GreencardContext) _context));
            }
        }

        public IPatientEncounterRepository PatientEncounterRepository
        {
            get { return _patientEncounterRepository??(_patientEncounterRepository=new PatientEncounterRepository((GreencardContext)_context));}
        }
        public IPatientLabTrackerRepository PatientLabTrackerRepository
        {
            get { return _patientLabTrackerRepository ?? (_patientLabTrackerRepository = new PatientLabTrackerRepository((GreencardContext)_context)); }
        }
        public IPatientLabOrderRepository PatientLabOrderRepository
        {
            get { return _patientLabOrderRepository ?? (_patientLabOrderRepository = new PatientLabOrderRepository((GreencardContext)_context)); }
        }

        public IPatientEnrollmentRepository PatientEnrollmentRepository
        {
            get { return _patientEnrollmentRepository??(_patientEnrollmentRepository=new PatientEnrollmentRepository((GreencardContext)_context));}
        }

        public IPatientRepository PatientRepository
        {
            get { return _patientRepository ?? (_patientRepository = new Patient.PatientRepository((GreencardContext)_context));  }
        }

        public IPatientIdentifierRepository PatientIdentifierRepository
        {
            get { return _patientIdentifierRepository ?? (_patientIdentifierRepository = new PatientIdentifierRepository((GreencardContext)_context)); }
        }

        public IPatientCareEndingRepository PatientCareEndingRepository
        {
            get
            {
                return _patientCareEndingRepository ??
                       (_patientCareEndingRepository = new PatientCareEndingRepository((GreencardContext) _context));
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
            get {return _patientVitalsRepository ?? (_patientVitalsRepository = new PatientVitalsRepository((GreencardContext)_context)); }
        }

        public IPatientArvHistoryRepository PatientArvHistoryRepository
        {
            get { return  _patientArvHistoryRepository ?? (_patientArvHistoryRepository=new PatientArvHistoryRepository((GreencardContext)_context)); }
        }

        public IPatientHivDiagnosisRepository PatientDiagnosisHivHistoryRepository
        {
            get
            {
                return _patientDiagnosisHivHistoryRepository ??  (_patientDiagnosisHivHistoryRepository = new PatientHivDiagnosisRepository((GreencardContext) _context));
            }
        }

        public IPatientHivEnrollmentBaselineRepository PatientHivEnrollmentBaselineRepository
        {
            get
            {
                return _patientHivEnrollmentBaselineRepository ?? (_patientHivEnrollmentBaselineRepository =  new PatientHivEnrollmentBaselineRepository((GreencardContext) _context));
            }
        }

        public IPatientTransferInRepository PatientTransferInRepository
        {
            get { return _patientTransferInRepository??(_patientTransferInRepository=new PatientTransferInRepository((GreencardContext)_context));}
        }

        public IPatientBaselineAssessmentRepository PatientBaselineAssessmentRepository
        {
            get {  return _patientBaselineAssessmentRepository??(_patientBaselineAssessmentRepository=new PatientBaselineAssessmentRepository((GreencardContext)_context));}
        }

        public IPatientLookupRepository PatientLookupRepository
        {
            get { return _patientLookupRepository??(_patientLookupRepository=new PatientLookupRepository((LookupContext)_context));}
        }

        public IPersonLookUpRepository PersonLookUpRepository
        {
            get
            {
                return _personLookUpRepository ??
                       (_personLookUpRepository = new PersonLookUpRepository((LookupContext) _context));
            }
        }

        public IPersonContactLookUpRepository PersonContactLookUpRepository
        {
            get
            {
                return _personContactLookUpRepository ??
                       (_personContactLookUpRepository = new PersonContactLookUpRepository((LookupContext) _context));
            }
        }

        public IPatientAppointmentRepository PatientAppointmentRepository
        {
            get {return _patientAppointmentRepository??(_patientAppointmentRepository = new PatientAppointmentRepository((GreencardContext)_context));}
        }

        public IPatientTreatmentInitiationRepository PatientTreatmentInitiationRepository
        {
            get
            {
                return _patientTreatmentInitiationRepository ??
                       (_patientTreatmentInitiationRepository =
                           new PatientTreatmentInitiationRepository((GreencardContext) _context));
            }
        }
        public IPatientHvTestingRepository PatientHivTestingRepository
        {
            get {return _patientHivTestingRepository??(_patientHivTestingRepository = new PatientHivTestingRepository((GreencardContext)_context));}
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public  void  Dispose()
        {
            _context.Dispose();
        }

        
    }
}
