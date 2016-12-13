using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using Config.Core.Interfaces;
using Config.Data.Repository;
using NUnit.Core;
using NUnit.Framework;
using Unitofwork.Core.Interface;
using Unitofwork.data.Repository;


namespace Config.Data.Tests
{
    [TestFixture]
    public  class ServiceAreaRepositoryTests
    {
        private ConfigContext _context;
        private IUnitOfWork _unitOfWork;


        [SetUp]
        public void SetUp()
        {
            //set up my test
            _context = new ConfigContext("name=IQCareDatabase");
            _unitOfWork = new UnitOfWork(_context);
        }

        [Test]
        public void should_GetById()
        {
            
var service = _unitOfWork.ServiceAreaRepository.GetById(1);
            Assert.NotNull(service);
            Debug.Print(service.ToString());
        }

        [Test]
        public void should_GetAll()
        {
            var services = _unitOfWork.ServiceAreaRepository.GetAll();
           Assert.That(services,Is.Not.Empty);
            foreach (var service in services)
            {
                Debug.Print(service.ToString());
            }
        }

        [Test]
        public void should_GetByCode()
        {
            var service = _unitOfWork.ServiceAreaRepository.GetByCode("CCC");
            Assert.NotNull(service);
            Debug.Print(service.ToString());
        }

        [TearDown]
        public void TearDown()
        {
            //delete any test data
        }
    }
}
