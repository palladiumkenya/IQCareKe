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


namespace Config.Data.Tests
{
    [TestFixture]
    public  class ServiceAreaRepositoryTests
    {
        private IServiceAreaRepository _serviceAreaRepository;
        private ConfigContext _context;


        [SetUp]
        public void SetUp()
        {
            //set up my test
            _context = new ConfigContext("name=IQCareDatabase");
            _serviceAreaRepository = new ServiceAreaRepository(_context);
        }

        [Test]
        public void should_GetById()
        {
            var service = _serviceAreaRepository.GetById(1);
            Assert.NotNull(service);
            Debug.Print(service.ToString());
        }

        [Test]
        public void should_GetAll()
        {
            var services = _serviceAreaRepository.GetAll();
           Assert.That(services,Is.Not.Empty);
            foreach (var service in services)
            {
                Debug.Print(service.ToString());
            }
        }

        [Test]
        public void should_GetByCode()
        {
            var service = _serviceAreaRepository.GetByCode("CCC");
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
