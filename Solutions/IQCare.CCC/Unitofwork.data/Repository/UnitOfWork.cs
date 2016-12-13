
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

    
    class UnitOfWork : IUnitOfWork
    {
        
        private readonly BaseContext _context;
        private IPatientRepository _patientRepository;
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
