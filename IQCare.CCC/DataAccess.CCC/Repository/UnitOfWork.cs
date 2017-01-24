using System;
using DataAccess.CCC.Context;
using DataAccess.Context;
using DataAccess.Context.ModuleMaster;
using DataAccess.CCC.Interface;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.CCC.Interface.person;
using DataAccess.CCC.Repository.Lookup;
using DataAccess.CCC.Repository.person;
using DataAccess.CCC.Repository.Patient;

namespace DataAccess.CCC.Repository
{
    public class UnitOfWork : IUnitOfWork
    {  
        private readonly BaseContext _context;

        //Person Interface
        private IPersonRepository _personRepository;
        private IPersonLocationRepository _personLocationRepository;
        private IPersonContactRepository _personContactRepository;
        private IPersonRelationshipRepository _personRelationshipRepository;
        private IPatientMaritalStatusRepository _patientMaritalStatusRepository;
        private IPatientOvcStatusRepository _patientOvcStatusRepository;
        private IPatientPopulationRepository _patientPopulationRepository;

        // Modules
        private IModuleRepository _moduleRepository;

        //lookupContext
        private ILookupRepository _lookupRepository;
        private ILookupMasterRepository _lookupMasterRepository;

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
