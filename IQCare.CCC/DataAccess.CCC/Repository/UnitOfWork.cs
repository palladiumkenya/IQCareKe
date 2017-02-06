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
using DataAccess.CCC.Repository.Lookup;
using DataAccess.CCC.Repository.person;
using DataAccess.CCC.Repository.Patient;
using DataAccess.CCC.Repository.visit;
using DataAccess.CCC.Repository.Enrollment;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.CCC.Repository.Baseline;

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

        /* visit */
        private IPatientMasterVisitRepository _patientMasterVisitRepository;
        private IPatientEncounterRepository _patientEncounterRepository;

        /* Enrollment */
        private IPatientEnrollmentRepository _patientEnrollmentRepository;
        private IPatientEntryPointRepository _patientEntryPointRepository;
        private IPatientIdentifierRepository _patientIdentifierRepository;
        /* Patient */
        private IPatientRepository _patientRepository;

        /*Baseline*/
       // private IPatientDisclosureRepository _patientDisclosureRepository;
        private IPatientArvHistoryRepository _patientArvHistoryRepository;
        private IPatientDiagnosisHivHistoryRepository _patientDiagnosisHivHistoryRepository;
        private IPatientDisclosureRepository _patientDisclosureRepository;
        private IINHProphylaxisRepository _inhProphylaxisRepository;
        private IPatientHivEnrollmentBaselineRepository _patientHivEnrollmentBaselineRepository;
        private IPatientTransferInRepository _patientTransferInRepository;
        private IPatientTreatmentInitiationRepository _patientTreatmentInitiationRepository;


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

        public IPatientVitalsRepository PatientVitalsRepository
        {
            get {return _patientVitalsRepository ?? (_patientVitalsRepository = new PatientVitalsRepository((GreencardContext)_context)); }
        }

        public IPatientArvHistoryRepository PatientArvHistoryRepository
        {
            get { return  _patientArvHistoryRepository ?? (_patientArvHistoryRepository=new PatientArvHistoryRepository((GreencardContext)_context)); }
        }

        public IPatientDiagnosisHivHistoryRepository PatientDiagnosisHivHistoryRepository
        {
            get
            {
                return _patientDiagnosisHivHistoryRepository ??  (_patientDiagnosisHivHistoryRepository = new PatientDiagnosisHivHistoryRepository((GreencardContext) _context));
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

        public IPatientTreatmentInitiationRepository PatientTreatmentInitiationRepository
        {
            get {  return _patientTreatmentInitiationRepository??(_patientTreatmentInitiationRepository=new PatientTreatmentInitiationRepository((GreencardContext)_context));}
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
