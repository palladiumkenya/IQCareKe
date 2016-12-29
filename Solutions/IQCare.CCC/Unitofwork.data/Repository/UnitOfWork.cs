
using System;
using Common.Data;
using Config.Core.Interfaces;
using Config.Data;
using Config.Data.Repository;
using Unitofwork.Core.Interface;
using PatientManagement.Core.Interfaces;
using PatientManagement.Data;
using PatientManagement.Data.Repository;
using VisitManagement.Core.Interfaces;
using VisitManagement.Data;
using VisitManagement.Data.Repository;
using PersonManagement.Core.Interfaces;
using PersonManagement.Data;
using PersonManagement.Data.Repository;

namespace Unitofwork.data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        
        private readonly BaseContext _context;

        private IPersonRepository _personRepository;
        private IPersonContactRepository _personContactRepository;
        private IPersonLocationRepository _personLocationRepository;
        private IPersonRelationshipRepository _personRelationshipRepository;

        private IPatientRepository _patientRepository;
        private IPatientEnrollmentRepository _patientEnrollmentRepository;
        private IPatientMaritalStatusRepository _patientMaritalStatusRepository;
        private IPatientOVCStatusRepository _patientOvcStatusRepository;
        private IPatientPopulationRepository _patientPopulationRepository;
        private IPatientTreatmentSupporterRepository _patientTreatmentSupporterRepository;
        private IPatientMasterVisitRepository _patientMasterVisitRepository;
        private IPatientEncounterRepository _patientEncounterRepository;

        private IServiceAreaRepository _serviceAreaRepository;

        public UnitOfWork(BaseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            _context = context;
           
        }

        public int Complete()
        {
           return  _context.SaveChanges();
        }

        public IPersonRepository PersonRepository
        {
            get { return _personRepository ?? (_personRepository = new PersonRepository((PersonContext) _context)); }
        }

      

        public IPersonLocationRepository PersonLocationRepository
        {
            get
            {
                return _personLocationRepository ??
                       (_personLocationRepository = new PersonLocationRepository((PersonContext) _context));
            }
        }

        public IPersonContactRepository PersonContactRepository {
            get
            {
                return _personContactRepository ??
                       (_personContactRepository = new PersonContactRepository((PersonContext) _context));
            } 
        }

        public IPersonRelationshipRepository PersonRelationshipRepository
        {
            get
            {
                return _personRelationshipRepository ??
                       (_personRelationshipRepository = new PersonRelationshipRepository((PersonContext) _context));
            }
        }


        public IPatientRepository PatientRepository
        {
            get
            {
                return _patientRepository ?? (_patientRepository = new PatientRepository((PatientContext) _context));
            }
        }

        public IPatientEnrollmentRepository PatientEnrollmentRepository
        {
            get
            {
                return _patientEnrollmentRepository ??
                       (_patientEnrollmentRepository = new PatientEnrollmentRepository((PatientContext) _context));
            }
        }


        public IPatientMaritalStatusRepository PatientMaritalStatusRepository
        {
            get
            {
                return _patientMaritalStatusRepository ??
                       (_patientMaritalStatusRepository = new PatientMaritalStatusRepository((PatientContext) _context));
            }
        }

        public IPatientOVCStatusRepository PatientOvcStatusRepository
        {
            get
            {
                return _patientOvcStatusRepository ??
                       (_patientOvcStatusRepository = new PatientOVCStatusRepository((PatientContext) _context));
            }
        }

        public IPatientPopulationRepository PatientPopulationRepository
        {
            get
            {
                return _patientPopulationRepository ??
                       (_patientPopulationRepository = new PatientPopulationRepository((PatientContext) _context));
            }
        }

        public IPatientTreatmentSupporterRepository PatientTreatmentSupporterRepository
        {
            get
            {
                return _patientTreatmentSupporterRepository ??
                       (_patientTreatmentSupporterRepository =
                           new PatientTreatmentSupporterRepository((PatientContext) _context));
            }
        }

        public IServiceAreaRepository ServiceAreaRepository
        {
            get
            {
                return _serviceAreaRepository ??
                       (_serviceAreaRepository = new ServiceAreaRepository((ConfigContext) _context));
            }
        }

        public IPatientMasterVisitRepository PatientmasterVisitRepository
        {
            get
            {
                return _patientMasterVisitRepository ??
                       (_patientMasterVisitRepository = new PatientMasterVisitRepository((VisitContext) _context));
            }
        }

        public IPatientEncounterRepository PatientEncounterRepository
        {
            get
            {
                return _patientEncounterRepository ?? (_patientEncounterRepository = new PatientEncounterRepository((VisitContext) _context));
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
