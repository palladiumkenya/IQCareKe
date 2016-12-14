
using System;
using Common.Data;
using Config.Core.Interfaces;
using Config.Data;
using Config.Data.Repository;
using Unitofwork.Core.Interface;
using PatientManagement.Core.Interfaces;
using PatientManagement.Data;
using PatientManagement.Data.Repository;

namespace Unitofwork.data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        
        private readonly BaseContext _context;
        private IPatientRepository _patientRepository;
        private IPatientContactRepository _patientContactRepository;
        private IPatientEnrollmentRepository _patientEnrollmentRepository;
        private IPatientLocationRepository _patientLocationRepository;
        private IPatientMaritalStatusRepository _patientMaritalStatusRepository;
        private IPatientOVCStatusRepository _patientOvcStatusRepository;
        private IPatientPopulationRepository _patientPopulationRepository;
        private IPatientTreatmentSupporterRepository _patientTreatmentSupporterRepository;

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

        public IPatientRepository PatientRepository
        {
            get { return _patientRepository ?? (_patientRepository = new PatientRepository((PatientContext) _context)); }

        }

        public IPatientContactRepository PatientContactRepository
        {
            get { return _patientContactRepository ?? (_patientContactRepository = new PatientContactRepository((PatientContext) _context));}
        }

        public IPatientEnrollmentRepository PatientEnrollmentRepository
        {
            get {return _patientEnrollmentRepository ??(_patientEnrollmentRepository=new PatientEnrollmentRepository((PatientContext) _context));}
        }

        public IPatientLocationRepository PatientLocationRepository
        {
            get { return _patientLocationRepository ?? (_patientLocationRepository = new PatientLocationRepository((PatientContext)_context)); }
        }

        public IPatientMaritalStatusRepository PatientMaritalStatusRepository
        {
            get { return _patientMaritalStatusRepository ?? (_patientMaritalStatusRepository = new PatientMaritalStatusRepository((PatientContext)_context)); }
        }

        public IPatientOVCStatusRepository PatientOvcStatusRepository
        {
            get { return _patientOvcStatusRepository ?? (_patientOvcStatusRepository = new PatientOVCStatusRepository((PatientContext)_context)); }
        }

        public IPatientPopulationRepository PatientPopulationRepository
        {
            get { return _patientPopulationRepository ?? (_patientPopulationRepository = new PatientPopulationRepository((PatientContext)_context)); }
        }

        public IPatientTreatmentSupporterRepository PatientTreatmentSupporterRepository
        {
            get { return _patientTreatmentSupporterRepository ?? (_patientTreatmentSupporterRepository = new PatientTreatmentSupporterRepository((PatientContext)_context)); }
        }

        public IServiceAreaRepository ServiceAreaRepository
        {
            get { return _serviceAreaRepository ?? (_serviceAreaRepository = new ServiceAreaRepository((ConfigContext)_context)); }

        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
