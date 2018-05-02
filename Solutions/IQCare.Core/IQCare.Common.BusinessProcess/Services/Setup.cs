using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.Services
{
    public class Setup
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public Setup(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<List<Facility>> GetActiveFacilities()
        {
            try
            {
                var facilities = await _unitOfWork.Repository<Facility>().Get(x => x.DeleteFlag == 0).ToListAsync();
                return facilities;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
        }

        public async Task<List<User>> GetActiveUsers()
        {
            try
            {
                var users = await _unitOfWork.Repository<User>().Get(x => x.DeleteFlag == 0).ToListAsync();
                return users;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
        }
    }
}