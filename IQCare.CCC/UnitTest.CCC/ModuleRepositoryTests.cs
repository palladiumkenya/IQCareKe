
using NUnit.Core;
using NUnit.Framework;
using System.Diagnostics;
using DataAccess.CCC.Interfaces;
using DataAccess.CCC;
using DataAccess.CCC.Repository;

namespace UnitTest.CCC
{
    [TestFixture]
    public class ModuleRepositoryTests
    {
        private GreencardContext _context;
        private IUnitOfWork _unitOfWork;


        [SetUp]
        public void SetUp()
        {
            //set up my test
            _context = new GreencardContext("name=IQCareDatabase");
            _unitOfWork = new UnitOfWork(_context);
        }

        [Test]
        public void should_GetById()
        {

            var module = _unitOfWork.ModuleRepository.GetById(1);
            Assert.NotNull(module);
            Debug.Print(module.ToString());
        }

        [Test]
        public void should_GetAll()
        {
            var modules = _unitOfWork.ModuleRepository.GetAll();
            Assert.That(modules, Is.Not.Empty);
            foreach (var module in modules)
            {
                Debug.Print(module.ToString());
            }
        }

        [Test]
        public void should_GetByCode()
        {
            var service = _unitOfWork.ModuleRepository.GetByCode("GreenCard");
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
