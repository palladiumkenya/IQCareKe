using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.Services
{
    public class PersonLocationService
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public PersonLocationService(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<PersonLocation> AddPersonLocation(PersonLocation personLocation)
        {
            try
            {
                await _unitOfWork.Repository<PersonLocation>().AddAsync(personLocation);
                await _unitOfWork.SaveAsync();
                return personLocation;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<List<PersonLocation>> GetPersonLocation(int personId)
        {
            try
            {
                List<PersonLocation> personLocations = await _unitOfWork.Repository<PersonLocation>().Get(x => x.PersonId == personId && x.DeleteFlag == false)
                    .ToListAsync();
                return personLocations;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<PersonLocation> UpdatePersonLocation(PersonLocation personLocation)
        {
            try
            {
                _unitOfWork.Repository<PersonLocation>().Update(personLocation);
                await _unitOfWork.SaveAsync();
                return personLocation;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " +e.InnerException);
                throw e;
            }
        }
    }
}