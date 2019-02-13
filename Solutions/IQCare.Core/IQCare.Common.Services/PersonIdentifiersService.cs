using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.Services
{
    public class PersonIdentifiersService
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        public PersonIdentifiersService(ICommonUnitOfWork commonUnitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<List<PersonIdentifier>> GetPersonIdentifierByType(int identifierTypeId, int personId)
        {
            try
            {
                var personIdentifiers = await _commonUnitOfWork.Repository<PersonIdentifier>().Get(x =>
                        x.DeleteFlag == false && x.IdentifierId == identifierTypeId && x.PersonId == personId)
                    .ToListAsync();
                
                return personIdentifiers;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task UpdatePersonIdentifierType(PersonIdentifier personIdentifierType)
        {
            try
            {
                _commonUnitOfWork.Repository<PersonIdentifier>().Update(personIdentifierType);
                await _commonUnitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<PersonIdentifier> AddPersonIdentifierType(int requestPersonId, int requestIdentifierId, string requestIdentifierValue, int requestUserId)
        {
            try
            {
                PersonIdentifier personIdentifier = new PersonIdentifier()
                {
                    PersonId = requestPersonId,
                    IdentifierId = requestIdentifierId,
                    IdentifierValue = requestIdentifierValue,
                    CreatedBy = requestUserId,
                    CreateDate = DateTime.Now,
                    DeleteFlag = false
                };

                await _commonUnitOfWork.Repository<PersonIdentifier>().AddAsync(personIdentifier);
                await _commonUnitOfWork.SaveAsync();

                return personIdentifier;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}