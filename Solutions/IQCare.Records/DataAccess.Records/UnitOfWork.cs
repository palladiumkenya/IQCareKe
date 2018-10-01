using System;
using DataAccess.Records.Interface;
using DataAccess.Context;
using System.Data.Entity;
using DataAccess.Records.Repository;
using DataAccess.Records.Context;
using DataAccess.Records.Repository.Lookup;

using DataAccess.Records.Repository.Patient;
using DataAccess.Records.Repository.Visit;
namespace DataAccess.Records
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private BaseContext _context;
        private IPersonRepository _personRepository;
        private IPersonRelationshipRepository _personRelationshipRepository;
        private IPatientMaritalStatusRepository _patientMaritalStatusRepository;
        private IPersonLocationRepository _personLocationRepository;
        private ILookupCounty _lookupCounty;
        private IPersonContactRepository _personContactRepository;
        private IPatientLookupRepository _patientLookupRepository;
        private ILookupRepository _lookupRepository;
        private IPatientRepository _patientRepository;
        private IPatientRegistrationLookupRepository _patientRegistrationLookupRepository;
        private IPatientVisitRepository _patientVisitRepository;
        private IPersonContactLookUpRepository _personContactLookUpRepository;
        private IPatientMasterVisitRepository _patientMasterVisitRepository;
        private IPatientEnrollmentRepository _patientEnrollmentRepository;
        private IPatientIdentifierRepository _patientIdentifierRepository;
        private IPatientEntryPointRepository _patientEntryPointRepository;
        private IPersonLookUpRepository _personLookUpRepository;
        private ILookupFacility _lookupFacilityRepository;

        private IIdentifierRepository _identifierRepository;
        private IPatientReEnrollmentRepository _patientReEnrollmentRepository;
        private IPersonIdentifierRepository _personIdentifierRepository;
        private IServiceAreaIdentifiersRepository _serviceAreaIdentifiersRepository;
        private IPatientEncounterRepository _patientEncounterRepository;
        private IPersonEmergencyContactRepository _personEmergencyContactRepository;
        private IPersonEducationRepository _personEducationRepository;
        private IPersonOccupationRepository _personOccupationRepository;
        private IPatientConsentRepository _patientConsentRepository;
        private IServiceAreaIndicatorRepository _serviceAreaIndicatorRepository;
        public DbContext Context { get { return _context; } }
        public  UnitOfWork(BaseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("missing context");
            }
            _context = context;
        }
          public  int Complete()
        {
            return _context.SaveChanges();
        }
        public ILookupFacility LookupFacilityRepository
        {
            get { return _lookupFacilityRepository ?? (_lookupFacilityRepository = new LookupFacilityRepository((LookupContext)_context)); }
        }
        public IServiceAreaIndicatorRepository ServiceAreaIndicatorRepository
        {
            get { return _serviceAreaIndicatorRepository ?? (_serviceAreaIndicatorRepository = new ServiceAreaIndicatorRepository((RecordContext)_context)); }
        }

        public IPatientConsentRepository PatientConsentRepository
        {
            get { return _patientConsentRepository ?? (_patientConsentRepository = new PatientConsentRepository((RecordContext)_context)); }
        }
        public IPatientReEnrollmentRepository PatientReEnrollmentRepository
        {
            get { return _patientReEnrollmentRepository ?? (_patientReEnrollmentRepository = new PatientReEnrollmentRepository((RecordContext)_context)); }
        }
        public IPersonEducationRepository PersonEducationRepository
        {
            get { return _personEducationRepository ?? (_personEducationRepository = new PersonEducationRepository((RecordContext)_context)); }
        }
        public IPersonOccupationRepository PersonOccupationRepository
        {
            get { return _personOccupationRepository ?? (_personOccupationRepository = new PersonOccupationRepository((RecordContext)_context)); }
        }

        public IPersonEmergencyContactRepository PersonEmergencyContactRepository
        {
            get { return _personEmergencyContactRepository ?? (_personEmergencyContactRepository = new PersonEmergencyContactRepository((RecordContext)_context)); }
        }
        public IPatientEncounterRepository PatientEncounterRepository
        {
            get { return _patientEncounterRepository ?? (_patientEncounterRepository = new PatientEncounterRepository((RecordContext)_context)); }

        }
        public IServiceAreaIdentifiersRepository ServiceAreaIdentifiersRepository
        {
            get { return _serviceAreaIdentifiersRepository ?? (_serviceAreaIdentifiersRepository = new ServiceAreaIdentifiersRepository((RecordContext)_context)); }
        }

        public IPersonIdentifierRepository PersonIdentifierRepository
        {
            get { return _personIdentifierRepository ?? (_personIdentifierRepository = new PersonIdentifierRepository((RecordContext)_context)); }
        }
      
        public IIdentifierRepository IdentifierRepository
        { get { return _identifierRepository ?? (_identifierRepository = new IdentifierRepository((RecordContext)_context)); } }

        public IPersonLookUpRepository PersonLookUpRepository
        {
            get { return _personLookUpRepository ?? (_personLookUpRepository = new PersonLookUpRepository((LookupContext)_context)); }
        }
        public IPatientEntryPointRepository PatientEntryPointRepository
        {
            get { return _patientEntryPointRepository ?? (_patientEntryPointRepository = new PatientEntrypointRepository((RecordContext)_context)); }
        }

        public IPatientIdentifierRepository PatientIdentifierRepository
        {
            get { return _patientIdentifierRepository ?? (_patientIdentifierRepository = new PatientIdentifierRepository((RecordContext)_context)); }
        }
        public IPatientVisitRepository PatientVisitRepository
        {
            get { return _patientVisitRepository ?? (_patientVisitRepository = new PatientVisitRepository((RecordContext)_context)); }
        }
        public IPersonRepository PersonRepository
        {
            get { return _personRepository ?? (_personRepository = new PersonRepository((PersonContext)_context)); }
        }
        
        public IPersonContactLookUpRepository PersonContactLookUpRepository
        {
            get { return _personContactLookUpRepository ?? (_personContactLookUpRepository = new PersonContactLookUpRepository((LookupContext)_context)); }
        }
        public IPatientEnrollmentRepository PatientEnrollmentRepository
        {
            get { return _patientEnrollmentRepository ?? (_patientEnrollmentRepository = new PatientEnrollmentRepository((RecordContext)_context)); }
        }
            public IPatientMasterVisitRepository PatientMasterVisitRepository
        {
            get { return _patientMasterVisitRepository ?? (_patientMasterVisitRepository = new PatientMasterVisitRepository((RecordContext)_context)); }
        }
        public IPatientRegistrationLookupRepository PatientRegistrationLookupRepository
        {
            get { return _patientRegistrationLookupRepository ?? (_patientRegistrationLookupRepository = new PatientRegistrationLookupRepository((LookupContext)_context)); }
        }
        public IPatientMaritalStatusRepository PatientMaritalStatusRepository
        {
            get { return _patientMaritalStatusRepository ?? (_patientMaritalStatusRepository = new PatientMaritalStatusRepository((PersonContext)_context)); }
        }

        public ILookupCounty LookupCountyRepository
        {
            get
            {
                return _lookupCounty ?? (_lookupCounty = new LookupCountyRepository((LookupContext)_context));
            }
        }

        public IPatientRepository PatientRepository
        {
            get { return _patientRepository ?? (_patientRepository = new Records.Repository.Patient.PatientRepository((RecordContext)_context)); }
        }


        public IPatientLookupRepository PatientLookupRepository
        {
            get { return _patientLookupRepository ?? (_patientLookupRepository = new PatientLookupRepository((LookupContext)_context)); }
        }
        public ILookupRepository LookupRepository
        {
            get { return _lookupRepository ?? (_lookupRepository = new LookupRepository((LookupContext)_context)); }
        }

        public IPersonRelationshipRepository PersonRelationshipRepository
        {
            get { return _personRelationshipRepository ?? (_personRelationshipRepository = new PersonRelationshipRepository((PersonContext)_context)); }
        }

        public IPersonLocationRepository PersonLocationRepository
        {
            get
            {
                return _personLocationRepository ??
                       (_personLocationRepository = new PersonLocationRepository((PersonContext)_context));
            }
        }


        public IPersonContactRepository PersonContactRepository
        {
            get
            {
                return _personContactRepository ??
                       (_personContactRepository = new PersonContactRepository((PersonContext)_context));
            }
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
