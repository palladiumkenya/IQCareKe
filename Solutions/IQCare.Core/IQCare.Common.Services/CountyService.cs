using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.Services
{
    public class CountyService
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        public CountyService(ICommonUnitOfWork commonUnitOfWork)
        {
            this._commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<List<County>> GetCounties()
        {
            try
            {
                var counties = await _commonUnitOfWork.Repository<County>().GetAllAsync();
                counties = counties.GroupBy(x => x.CountyId).Select(x => x.First()).OrderBy(l => l.CountyName);
                return counties.ToList();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        public async Task<List<SubCountyLookup>> GetSubCountyList(int countyId)
        {
            try
            {
                var result = await _commonUnitOfWork.Repository<SubCountyLookup>().Get(x => x.CountyId == countyId).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;

            }
        }
    }
}