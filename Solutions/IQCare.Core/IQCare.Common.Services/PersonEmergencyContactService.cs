using System;
using IQCare.Common.Infrastructure;

namespace IQCare.Common.Services
{
    public class PersonEmergencyContactService
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;

        public PersonEmergencyContactService(ICommonUnitOfWork commonUnitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }
    }
}