using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.Services
{
    public class PersonOccupationService
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        public PersonOccupationService(ICommonUnitOfWork commonUnitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<List<PersonOccupation>> GetCurrentOccupation(int personId)
        {
            try
            {
                var occupation = await _commonUnitOfWork.Repository<PersonOccupation>()
                    .Get(x => x.PersonId == personId && x.DeleteFlag == false).ToListAsync();
                return occupation;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<PersonOccupation> Add(PersonOccupation personOccupation)
        {
            try
            {
                await _commonUnitOfWork.Repository<PersonOccupation>().AddAsync(personOccupation);
                await _commonUnitOfWork.SaveAsync();
                return personOccupation;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<PersonOccupation> Update(PersonOccupation personOccupation)
        {
            try
            {
                _commonUnitOfWork.Repository<PersonOccupation>().Update(personOccupation);
                await _commonUnitOfWork.SaveAsync();
                return personOccupation;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }
    }
}