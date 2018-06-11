using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.Services
{
    public class LookupLogic
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public LookupLogic(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<string> GetLookupNameById(int itemId)
        {
            try
            {
                var result = await _unitOfWork.Repository<LookupItemView>().Get(x => x.ItemId == itemId)
                    .Select(y => y.ItemName).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
        }

        public async Task<int> GetLookupIdbyName(string name)
        {
            try
            {
                var result = await _unitOfWork.Repository<LookupItemView>().Get(x => x.ItemName == name).Select(y => y.ItemId).FirstOrDefaultAsync();
                return result;
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                return 0;
            }


        }
        public async Task<int> GetDecodeIdByName(string name, int codeId)
        {
            try
            {
                var result = await _unitOfWork.Repository<Decode>()
                    .Get(x => x.Name.ToLower().Contains(name.ToLower()) && x.CodeID == codeId).FirstOrDefaultAsync();

                return result.ID;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return 0;
            }
        }

        public async Task<List<SubCountyLookup>> GetSubCountyList(int CountyId)
        {
            try
            {
                var result = await _unitOfWork.Repository<SubCountyLookup>().Get(x => x.CountyId == CountyId).ToListAsync();
                return result.ToList();
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                throw;

            }
        }


        public async Task<List<WardLookup>> GetWardList(int subcountyId)
        {
            try
            {
                var result = await _unitOfWork.Repository<WardLookup>().Get(x => x.SubCountyId==subcountyId).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;

            }
        }
        public async Task<List<CountyLookup>> GetCountyList()
        {
            try
            {
                var result = await _unitOfWork.Repository<CountyLookup>().GetAllAsync();
                return result.ToList();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;

            }
        }




    }
}