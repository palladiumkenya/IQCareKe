using System;
using System.Data.Entity;
using DataAccess.Context;
using DataAccess.WebApi.Interface;

namespace DataAccess.WebApi.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private BaseContext _context;

        private IApiInboxRepository _apiInboxRepository;
        private IApiOutboxRepository _apiOutboxRepository;
        private IApiInteropSystemsRepository _apiInteropSystemsRepository;
        private ISmartCardPatientListRepository _smartCardPatientListRepository;
        private IPsmartStoreRepository _psmartStoreRepository;
        private IPSmartAuthRepository _psmartAuthRepository;
        private IShrRepository _shrRepository;

        private IPatientIdentificationRepository _patientIdentificationRepository;
        private IExternalPatientIdRepository _externalPatientIdRepository;
        private IInternalPatientIdRepository _internalPatientIdRepository;
        private IPatientNameRepository _patientNameRepository;
        private IPhysicalAddressRepository _physicalAddressRepository;
        private IPatientAddressRepository _patientAddressRepository;
        private IMotherNameRepository _motherNameRepository;
        private IMotherIdentifierRepository _motherIdentifierRepository;
        private IMotherDetailsRepository _motherDetailsRepository;
        private INokNameRepository _nokNameRepository;
        private INextOfKinRepository _nextOfKinRepository;
        private IProviderDetailsRepository _providerDetailsRepository;
        private IHivTestRepository _hivTestRepository;
        private IImmunizationRepository _immunizationRepository;
        private ICardDetailsRepository _cardDetailsRepository;
  
        private IFamilyInfoRepository _familyInfoRepository;
        private IHivTestTrackerRepository _hivTestTrackerRepository;
        private IImmunizationTrackerRepository _immunizationTrackerRepository;
        private IMstPatientReposiroty _mstPatientReposiroty;
        private IPatientProgramStartRepository _patientProgramStartRepository;
        private MotherDetailsViewRepository _motherDetailsViewRepository;
        IPSmartTransactionLogRepository _pSmartLogRepository;


        public UnitOfWork(BaseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("missing context");
            }
            _context = context;
           // _pSmartLogRepository = new PSmartLogRepository();
        }

        public UnitOfWork()
        {
        }

        //public PSmartLogRepository LogRepository => _pSmartLogRepository ?? (_pSmartLogRepository = new PSmartLogRepository((PsmartContext)_context));

        public IPSmartTransactionLogRepository LogRepository
        {
            get
            {
                return _pSmartLogRepository ??                    (_pSmartLogRepository = new PSmartLogRepository((PsmartContext)_context));
            }
        }
        public DbContext Context { get { return _context; } }

        public IApiInboxRepository ApiInboxRepository
        {
            get
            {
                return _apiInboxRepository ?? (_apiInboxRepository = new ApiInboxRepository((ApiContext)_context));
            }
        }

        public IApiOutboxRepository ApiOutboxRepository
        {
            get
            {
                return _apiOutboxRepository ?? (_apiOutboxRepository = new ApiOutboxRepository((ApiContext)_context));
            }
        }

        public IApiInteropSystemsRepository ApiInteropSystemsRepository
        {
            get
            {
                return _apiInteropSystemsRepository ??
                       (_apiInteropSystemsRepository = new ApiInteropSystemRepository((ApiContext)_context));
            }
        }

        public ISmartCardPatientListRepository SmartCardPatientListRepository
        {
            get
            {
                return _smartCardPatientListRepository ?? (_smartCardPatientListRepository =
                           new SmartCardPatientListRepository((PsmartContext)_context));
            }
        }

        public IPsmartStoreRepository PSmartStoreRepository
        {
            get
            {
                return _psmartStoreRepository ??
                       (_psmartStoreRepository = new PsmartRepository((PsmartContext)_context));
            }
        }
        public IPSmartAuthRepository PSmartAuthRepository
        {
            get
            {
                return _psmartAuthRepository ??
                       (_psmartAuthRepository = new PSmartAuthRepository((PsmartContext)_context));
            }
        }

        public IShrRepository ShrRepository 
        {
            get
            {
                return _shrRepository ??(_shrRepository = new ShrRepository((PsmartContext)_context));
            }
        }

        public IPatientIdentificationRepository PatientIdentificationRepository
        {
            get
            {
                return _patientIdentificationRepository ?? (_patientIdentificationRepository = new PatientIdentificationRepository((PsmartContext)_context));
            }
        }

        public IExternalPatientIdRepository ExternalPatientIdRepository
        {
            get
            {
                return _externalPatientIdRepository ?? (_externalPatientIdRepository = new ExternalPatientIdRepository((PsmartContext)_context));
            }
        }

        public IInternalPatientIdRepository InternalPatientIdRepository
        {
            get
            {
                return _internalPatientIdRepository ?? (_internalPatientIdRepository = new InternalPatientIdRepository((PsmartContext)_context));
            }
        }

        public IPatientNameRepository PatientNameRepository
        {
            get
            {
                return _patientNameRepository ?? (_patientNameRepository = new PatientNameRepository((PsmartContext)_context));
            }
        }

        public IPhysicalAddressRepository PhysicalAddressRepository
        {
            get
            {
                return _physicalAddressRepository ?? (_physicalAddressRepository = new PatientPhysicalAddressRepository((PsmartContext)_context));
            }
        }

        public IPatientAddressRepository PatientAddressRepository
        {
            get
            {
                return _patientAddressRepository ?? (_patientAddressRepository = new PatientAddressRepository((PsmartContext)_context));
            }
        }

        public IMotherNameRepository MotherNameRepository
        {
            get
            {
                return _motherNameRepository ?? (_motherNameRepository = new MotherNameRepository((PsmartContext)_context));
            }
        }

        public IMotherIdentifierRepository MotherIdentifierRepository
        {
            get
            {
                return _motherIdentifierRepository ?? (_motherIdentifierRepository = new MotherIdentifierRepository((PsmartContext)_context));
            }
        }

        public IMotherDetailsRepository MotherDetailsRepository
        {
            get
            {
                return _motherDetailsRepository ??
                       (_motherDetailsRepository = new MotherDetailsRepository((PsmartContext)_context));
            }
        }

        public INokNameRepository NokNameRepository
        {
            get { return _nokNameRepository ?? (_nokNameRepository = new NokNameRepository((PsmartContext)_context)); }
        }

        public INextOfKinRepository NextOfKinRepository
        {
            get
            {
                return _nextOfKinRepository ??
                       (_nextOfKinRepository = new NextOfKinRepository((PsmartContext)_context));
            }
        }

        public IProviderDetailsRepository ProviderDetailsRepository
        {
            get
            {
                return _providerDetailsRepository ??
                       (_providerDetailsRepository = new ProviderDetailsRepository((PsmartContext)_context));
            }
        }

        public IHivTestRepository HivTestRepository
        {
            get { return _hivTestRepository ?? (_hivTestRepository = new HivTestRepository((PsmartContext)_context)); }
        }


        public IImmunizationRepository ImmunizationRepository
        {
            get { return _immunizationRepository ?? (_immunizationRepository = new ImmunizationRepository((PsmartContext)_context)); }
        }

        public ICardDetailsRepository CardDetailsRepository
        {
            get { return _cardDetailsRepository ?? (_cardDetailsRepository = new CardDetailsRepository((PsmartContext)_context)); }
        }
        public IFamilyInfoRepository FamilyInfoRepository
        {
            get
            {
                return _familyInfoRepository ??(_familyInfoRepository = new FamilyInfoRepository((PsmartContext) _context));
            }
        }

        public IHivTestTrackerRepository HivTestTrackerRepository
        {
            get
            {
                return _hivTestTrackerRepository ??(_hivTestTrackerRepository = new HivTestTrackerRepository((PsmartContext) _context));
            }
        }

        public IImmunizationTrackerRepository ImmunizationTrackerRepository
        {
            get
            {
                return _immunizationTrackerRepository ?? (_immunizationTrackerRepository =new ImmunizationTrackerRepository((PsmartContext) _context));
            }
        }

        public IMstPatientReposiroty MstPatientReposiroty
        {
            get {return _mstPatientReposiroty??(_mstPatientReposiroty=new MstPatientRepository((PsmartContext)_context));}
        }

        public IPatientProgramStartRepository PatientProgramStartRepository
        {
            get { return _patientProgramStartRepository ?? (_patientProgramStartRepository = new PatientProgramStartRepository((PsmartContext)_context)); }
        }

        public IMotherDetailsViewRepository MotherDetailsViewRepository
        {
            get { return _motherDetailsViewRepository ?? (_motherDetailsViewRepository = new MotherDetailsViewRepository((PsmartContext)_context)); }
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
