using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.Services
{
    public class EducationLevelService
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;

        public EducationLevelService(ICommonUnitOfWork commonUnitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<List<PersonEducation>> GetCurrentPersonEducation(int personId)
        {
            try
            {
                List<PersonEducation> personEducations = await _commonUnitOfWork.Repository<PersonEducation>()
                    .Get(x => x.PersonId == personId && x.DeleteFlag == false).ToListAsync();
                return personEducations;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<PersonEducation> AddPersonEducation(PersonEducation personEducation)
        {
            try
            {
                await _commonUnitOfWork.Repository<PersonEducation>().AddAsync(personEducation);
                await _commonUnitOfWork.SaveAsync();
                return personEducation;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PersonEducation> UpdatePersonEducation(PersonEducation pm)
        {
            try
            {
                _commonUnitOfWork.Repository<PersonEducation>().Update(pm);
                await _commonUnitOfWork.SaveAsync();
                return pm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}