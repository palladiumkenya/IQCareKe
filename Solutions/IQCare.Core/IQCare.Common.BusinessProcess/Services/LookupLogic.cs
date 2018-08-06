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

        public async Task<List<LookupItemView>> GetLookupNameByGroupNameItemId(int itemId, string masterName)
        {
            try
            {
                var result = await _unitOfWork.Repository<LookupItemView>()
                    .Get(x => x.ItemId == itemId && x.MasterName == masterName).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<List<string>> GetLookupNameById(int itemId)
        {
            try
            {
                var result = await _unitOfWork.Repository<LookupItemView>().Get(x => x.ItemId == itemId)
                    .Select(y => y.ItemName).ToListAsync();

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
    }
}